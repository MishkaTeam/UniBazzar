using Application.Aggregates.Customer;
using Domain;
using Domain.Aggregates.Users;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.User
{
    public class UserApplication(IUserRepository userRepository,IUnitOfWork unitOfWork)
    {
        public async Task<CreateUserViewModel> CreateAsync(CreateUserViewModel viewModel)
        {
            var entity = Domain.Aggregates.Users.User.Register
                (
                viewModel.FirstName,
                viewModel.LastName,
                viewModel.NationalCode,
                viewModel.Mobile,
                viewModel.Password,
                viewModel.Email
                );
            userRepository.AddUser(entity);
            await unitOfWork.CommitAsync();
            return entity.Adapt<CreateUserViewModel>();
        }

        public async Task<List<UpdateUserViewModel>> GetAllUser ()
        {
            var users= await userRepository.GetAllUsersAsync();
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
                updateViewModel.NationalCode,
                updateViewModel.Mobile
                );

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
    }
}
