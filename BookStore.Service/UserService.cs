using BookStore.Models.DAL;
using BookStore.Models.DAL.Interfaces;
using BookStore.Models.DataViewModel.Requests;
using BookStore.Models.DataViewModel.Responses;
using BookStore.Models.DTOs.Requests;
using BookStore.Models.DTOs.Responses;
using BookStore.Models.Entities;
using BookStore.Service.Base;
using BookStore.Service.Interfaces;

namespace BookStore.Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly Encryptor encryptor;
        private readonly IUserGroupRepository userGroupRepository;
        private readonly IEmailSender emailSender;
        private readonly ICartRepository cartRepository;

        public UserService(
               IUnitOfWork unitOfWork, 
               IUserRepository userRepository,
               Encryptor encryptor,
               IUserGroupRepository userGroupRepository,
               ICartRepository cartRepository,
               IEmailSender emailSender, IMapperCustom mapperCustom) : base(unitOfWork, mapperCustom)
        {
            this.userRepository = userRepository;
            this.encryptor = encryptor;
            this.userGroupRepository = userGroupRepository;
            this.emailSender = emailSender;
            this.cartRepository = cartRepository;
        }

        public async Task<bool> CheckUserByActivationCode(Guid activationCode)
        {
            var user = await userRepository.FindAsync(us => us.ActivationCode == activationCode);
            if (user == null)
                return false;

            user.IsActived = true;
            await unitOfWork.CommitTransaction();
            return true; 
        }

        public async Task<UserResponse> ForgotPassword(string userEmail)
        {
            try
            {
                // 1. Find user by email
                var user = await userRepository.FindAsync(us => us.Email == userEmail && us.IsActived == true);

                // 2. Check
                if (user == null)
                {
                    return new UserResponse
                    {
                        IsSuccess = false,
                        Message = "Không thể tìm thấy Email được đăng ký !",
                    };
                }

                // 3. Generate reset password code to validate
                var resetCode = Guid.NewGuid();
                user.ResetPasswordCode = resetCode;

                // 3. Send email to user to reset password
                await emailSender.SendEmailVerificationAsync(userEmail, resetCode.ToString(), "reset-password");
                await unitOfWork.CommitTransaction();

                return new UserResponse
                {
                    IsSuccess = true,
                };
            }
            catch (Exception e)
            {
                return new UserResponse
                {
                    IsSuccess = false,
                    Message = e.Message,
                };
            }
        }

        public async Task<bool> GetUserByResetCode(Guid resetPassCode)
        {
            return await userRepository.FindAsync(us => us.ResetPasswordCode == resetPassCode) != null;
        }

        public async Task<UserProfileResponse> GetUserProfile(Guid userId)
        {
            var user = await userRepository.FindAsync(us => us.Id ==  userId);
            if (user == null)
            {
                return new UserProfileResponse
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy người dùng"
                };
            }
            var userProfile = new UserProfileResponse
            {
                IsSuccess = true,
                CustomerFullName = user.Name,
                CustomerId = user.Id
            };
            return userProfile;
        }

        public async Task<UserResponse> Login(LoginRequest req)
        {
            //FindUser by User Name
            var user = await userRepository.FindAsync(em => em.Email!.Equals(req.Email));
            
            //Can't find UserName
            if (user == null) 
            {
                return new UserResponse
                {
                    IsSuccess = false,
                    Message = "Tài Khoản hoặc mật khẩu không đúng !"
                };
            }

            //Check if user isn't activated
            if(!user.IsActived)
            {
                return new UserResponse
                {
                    IsSuccess = false,
                    Message = "Kiểm tra mail đã đăng ký để kích hoạt tài khoản!"
                };
            }
            
            //Check password
            if(encryptor.MD5Hash(req.Password!) != user.Password)
            {
                return new UserResponse
                {
                    IsSuccess = false,
                    Message = "Sai mật khẩu hoặc tên đăng nhập !",
                };
            }
            return new UserResponse
            {
                User = user,
                IsSuccess = true,
            };
        }

        public async Task<UserResponse> Register(RegisterRequest req)
        {
            try
            {
                // 1. Check if duplicated account created
                var getUser = await userRepository.FindAsync(us => us.Email == req.Email && us.IsActived == true);

                if (getUser != null)
                {
                    return new UserResponse
                    {
                        IsSuccess = false,
                        Message = "Email đã được sử dụng !",
                    };
                }

                // 2. Check pass with confirm pass
                if (!String.Equals(req.Password, req.ConfirmPassWord))
                {
                    return new UserResponse
                    {
                        IsSuccess = false,
                        Message = "Mật khẩu xác nhận không khớp !",
                    };
                }

                // Create Account
                var user = new Account
                {
                    Name = req.Name,
                    Email = req.Email,
                    IsActived = false,
                    ActivationCode = Guid.NewGuid(),
                    DateCreated = DateTime.Now,
                    UserGroupId = userGroupRepository.FindAsync(gn => gn.Name!.Equals("Customer")).Result.Id,
                    Password = encryptor.MD5Hash(req.Password!)
                };

                await userRepository.AddAsync(user);
                var cart = new Cart
                {
                    AccountId = user.Id,
                };
                await cartRepository.AddAsync(cart);
                await unitOfWork.CommitTransaction();
                await emailSender.SendEmailVerificationAsync(user.Email!, user.ActivationCode.ToString(), "verify-account");
                return new UserResponse
                {
                    IsSuccess = true,
                };
            }
            catch (Exception e)
            {
                return new UserResponse
                {
                    IsSuccess = false,
                    Message = e.Message,
                };
            }
        }

        public async Task<UserResponse> ResetPassword(ResetPasswordRequest req)
        {
            try
            {
                // 1. Find user by reset password code
                var user = await userRepository.FindAsync(us => us.ResetPasswordCode == new Guid(req.ResetPasswordCode!) && us.IsActived == true);

                // 2. Check
                if (user == null)
                {
                    return new UserResponse
                    {
                        IsSuccess = false,
                        Message = "Không tìm thấy tài khoản !",
                    };
                }

                user.Password = encryptor.MD5Hash(req.NewPassword);
                user.ResetPasswordCode = new Guid();

                await unitOfWork.CommitTransaction();

                return new UserResponse
                {
                    IsSuccess = true,
                };
            }
            catch (Exception e)
            {
                return new UserResponse
                {
                    IsSuccess = false,
                    Message = e.Message,
                };
            }
        }
    }
}
