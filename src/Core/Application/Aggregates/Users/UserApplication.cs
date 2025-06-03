using Application.ViewModels.Authentication;
using Domain;
using Domain.Aggregates.Users;
using Framework.DataType;
using Mapster;

namespace Application.Aggregates.Users
{
    public class UserApplication(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        public async Task<CreateUserViewModel> CreateAsync(CreateUserViewModel viewModel)
        {
            var user = User.Register
                (
                viewModel.FirstName,
                viewModel.LastName,
                viewModel.Mobile,
                viewModel.Password,
                viewModel.UserName
                );

            user.SetUserRole(viewModel.Role);

            userRepository.AddAsync(user);

            await unitOfWork.SaveChangesAsync();

            return user.Adapt<CreateUserViewModel>();
        }

        public async Task<List<UpdateUserViewModel>> GetAllUser()
        {
            var users = await userRepository.GetAllAsync();
            return users.Adapt<List<UpdateUserViewModel>>();
        }

        public async Task<UpdateUserViewModel> GetUserAsync(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null || user.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }
            return user.Adapt<UpdateUserViewModel>();
        }

        public async Task<UpdateUserViewModel> UpdateAsync(UpdateUserViewModel updateViewModel)
        {
            var user = await userRepository.GetByIdAsync(updateViewModel.Id);

            if (user == null || user.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }

            user.Update(
                updateViewModel.FirstName,
                updateViewModel.LastName,
                updateViewModel.UserName,
                updateViewModel.Password,
                updateViewModel.Mobile
                );

            user.SetUserRole(updateViewModel.Role);

            await unitOfWork.SaveChangesAsync();
            return user.Adapt<UpdateUserViewModel>();
        }
        public async Task DeleteAsync(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null || user.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }

            await userRepository.RemoveAsync(user);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<ResultContract<UserViewModel>> LoginWithMobileAsync(LoginViewModel model)
        {
            var user = await userRepository.GetUserWithMobile(model.UserName);
            return LoginAsync(model, user);
        }

        public async Task<ResultContract<UserViewModel>> LoginWithUserNameAsync(LoginViewModel model)
        {
            var user = await userRepository.GetUserWithUserName(model.UserName);
            return LoginAsync(model, user);
        }

        private static ResultContract<UserViewModel> LoginAsync(LoginViewModel model, User user)
        {
            if (user == null || user.Id == Guid.Empty)
            {
                return (ErrorType.NotFound,
                    string.Format(Resources.Messages.Errors.NotFound, Resources.DataDictionary.User));
            }

            if (user.Password != model.Password) // Encryption
            {
                return (ErrorType.InvalidCredentials, Resources.Messages.Validations.Password);
            }

            return user.Adapt<UserViewModel>();
        }


    }
}
