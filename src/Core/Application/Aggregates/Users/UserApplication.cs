using Application.ViewModels.Authentication;
using Domain;
using Domain.Aggregates.Users;
using Domain.Aggregates.Users.Enums;
using Framework.DataType;
using Mapster;
using Resources;

namespace Application.Aggregates.Users
{
    public class UserApplication(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        public async Task<CreateUserViewModel> CreateAsync(CreateUserViewModel viewModel)
        {
            var entity = Domain.Aggregates.Users.User.Register
                (
                viewModel.FirstName,
                viewModel.LastName,
                viewModel.Mobile,
                viewModel.Password,
                viewModel.UserName
                );

            entity.SetUserRole(viewModel.Role);

            userRepository.AddUser(entity);
            await unitOfWork.CommitAsync();
            return entity.Adapt<CreateUserViewModel>();
        }

        public async Task<List<UpdateUserViewModel>> GetAllUser()
        {
            var users = await userRepository.GetAllUsersAsync();
            return users.Adapt<List<UpdateUserViewModel>>();
        }

        public async Task<UpdateUserViewModel> GetUserAsync(Guid id)
        {
            var user = await userRepository.GetUserAsync(id);

            if (user == null || user.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }
            return user.Adapt<UpdateUserViewModel>();
        }

        public async Task<UpdateUserViewModel> UpdateAsync(UpdateUserViewModel updateViewModel)
        {
            var entity = await userRepository.GetUserAsync(updateViewModel.Id);

            if (entity == null || entity.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }

            entity.Update(
                updateViewModel.FirstName,
                updateViewModel.LastName,
                updateViewModel.UserName,
                updateViewModel.Password,
                updateViewModel.Mobile
                );

            entity.SetUserRole(updateViewModel.Role);

            await unitOfWork.CommitAsync();
            return entity.Adapt<UpdateUserViewModel>();
        }
        public async Task DeleteAsync(Guid id)
        {
            var entity = await userRepository.GetUserAsync(id);

            if (entity == null || entity.Id == Guid.Empty)
            {
                throw new Exception(Resources.Messages.Errors.NotFound);
            }

            userRepository.Remove(entity);
            await unitOfWork.CommitAsync();
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
