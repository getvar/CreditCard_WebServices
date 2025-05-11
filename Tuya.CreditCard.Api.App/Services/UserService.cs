using AutoMapper;
using Tuya.CreditCard.Api.App.Contracts.Services;
using Tuya.CreditCard.Api.Common.Contracts;
using Tuya.CreditCard.Api.Common.Exceptions;
using Tuya.CreditCard.Api.Common.Helpers;
using Tuya.CreditCard.Api.DAL.Contracts.Entities;
using Tuya.CreditCard.Api.DAL.Contracts.Repositories;
using Tuya.CreditCard.Api.DAL.Mappers;
using Tuya.CreditCard.Api.DTO.Models;

namespace Tuya.CreditCard.Api.App.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IApiAccessorUserData _apiAccessorUserData;

        public UserService(IUserRepository userRepository, IMapper mapper, IApiAccessorUserData apiAccessorUserData)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _apiAccessorUserData = apiAccessorUserData;
        }

        public async Task<bool> AddUser(UserManage user)
        {
            string baseErrorMessage = "No fue posible crear el usuario.";
            await ValidateAddUserData(user, baseErrorMessage);
            var entity = UserMapper.MapAdd(user, _mapper);
            var createdUser = await _userRepository.AddAsync(entity);
            ValidateObjectHelper<UserEntity>.ValidateObject(createdUser, true, $"{baseErrorMessage} Por favor, Intente más tarde", new NotInsertedException(string.Empty));
            return true;
        }

        public async Task<User> GetUser()
        {
            var user = await _userRepository.GetByIdAsync(_apiAccessorUserData.GetUserId());
            ValidateObjectHelper<UserEntity>.ValidateObject(user, true, $"No se encontró información", new KeyNotFoundException(string.Empty));
            return _mapper.Map<User>(user);
        }

        public async Task<UserData> GetUserByUserName(string userName) => _mapper.Map<UserData>(await _userRepository.GetByUserName(userName));

        public async Task<bool> UpdateUser(UserManage user)
        {
            string baseErrorMessage = "No fue posible actualizar el usuario.";
            await ValidateUpdateUserData(user, baseErrorMessage);
            var entity = UserMapper.MapUpdate(user, _mapper);
            var updatedUser = await _userRepository.EditAsync(entity);
            ValidateObjectHelper<UserEntity>.ValidateObject(updatedUser, true, $"{baseErrorMessage} Por favor, Intente más tarde", new NotInsertedException(string.Empty));
            return true;
        }

        private async Task ValidateAddUserData(UserManage user, string baseErrorMessage)
        {
            ValidateGenericUserData(user, baseErrorMessage);
            ValidationHelper.ValidateEmptyString(user.Password, true, $"{baseErrorMessage} La CONTRASEÑA es obligatoria");

            if (await _userRepository.GetByUserName(user.UserName) != null)
                ExceptionHelper.GenerateException($"{baseErrorMessage} Ya existe un usuario con el CORREO enviado", new ArgumentException(string.Empty));
        }

        private async Task ValidateUpdateUserData(UserManage user, string baseErrorMessage)
        {
            if (user.Id == Guid.Empty)
                ExceptionHelper.GenerateException($"{baseErrorMessage} Debe enviar el ID del usuario", new ArgumentException(string.Empty));

            ValidateGenericUserData(user, baseErrorMessage);
            var existsUser = await _userRepository.GetByUserName(user.UserName);

            if (existsUser != null && !existsUser.Id.Equals(user.Id))
                ExceptionHelper.GenerateException($"{baseErrorMessage} Ya existe un usuario con el CORREO enviado", new ArgumentException(string.Empty));
        }

        private void ValidateGenericUserData(UserManage user, string baseErrorMessage)
        {
            ValidationHelper.ValidateEmptyString(user.Name, true, $"{baseErrorMessage} El NOMBRE es obligatorio");
            ValidationHelper.ValidateEmptyString(user.LastName, true, $"{baseErrorMessage} El APELLIDO es obligatorio");
            ValidationHelper.ValidateEmptyString(user.Phone, true, $"{baseErrorMessage} El TELÉFONO es obligatorio");
            ValidationHelper.ValidateEmptyString(user.Adrress, true, $"{baseErrorMessage} La DIRECCIÓN es obligatoria");
            ValidationHelper.ValidateEmptyString(user.UserName, true, $"{baseErrorMessage} El CORREO es obligatorio");
        }
    }
}
