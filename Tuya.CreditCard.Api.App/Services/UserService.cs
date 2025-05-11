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
        private readonly IMasterService _masterService;

        public UserService(IUserRepository userRepository, IMapper mapper, IApiAccessorUserData apiAccessorUserData, IMasterService masterService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _apiAccessorUserData = apiAccessorUserData;
            _masterService = masterService;
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

        public async Task<bool> UpdateUser(UserEdit user)
        {
            string baseErrorMessage = "No fue posible actualizar el usuario.";
            await ValidateUpdateUserData(user, _apiAccessorUserData.GetUserId(), baseErrorMessage);
            var entity = UserMapper.MapUpdate(user, _mapper);
            entity.Id = _apiAccessorUserData.GetUserId();
            var updatedUser = await _userRepository.EditAsync(entity);
            ValidateObjectHelper<UserEntity>.ValidateObject(updatedUser, true, $"{baseErrorMessage} Por favor, Intente más tarde", new NotInsertedException(string.Empty));
            return true;
        }

        private async Task ValidateAddUserData(UserManage user, string baseErrorMessage)
        {
            await ValidateGenericUserData(user.Name, user.LastName, user.Phone, user.Adrress, (int)user.IdentificationType, user.Identification, baseErrorMessage);
            ValidationHelper.ValidateEmptyString(user.UserName, true, $"{baseErrorMessage} El CORREO es obligatorio");
            ValidationHelper.ValidateEmptyString(user.Password, true, $"{baseErrorMessage} La CONTRASEÑA es obligatoria");

            if (await _userRepository.GetByUserName(user.UserName) != null)
                ExceptionHelper.GenerateException($"{baseErrorMessage} Ya existe un usuario con el CORREO enviado", new ArgumentException(string.Empty));
        }

        private async Task ValidateUpdateUserData(UserEdit user, Guid userId, string baseErrorMessage)
        {
            if (userId == Guid.Empty)
                ExceptionHelper.GenerateException($"{baseErrorMessage} El usuario no ha iniciado sesión", new ArgumentException(string.Empty));

            await ValidateGenericUserData(user.Name, user.LastName, user.Phone, user.Adrress, (int)user.IdentificationType, user.Identification, baseErrorMessage);
        }

        private async Task ValidateGenericUserData(string name, string lastName, string phone, string adrress, int identificationType, string identification, string baseErrorMessage)
        {
            if (!await _masterService.ExistsIdentificationType(identificationType))
                ExceptionHelper.GenerateException($"{baseErrorMessage} Debe enviar un TIPO DE IDENTIFICACIÓN válido", new ArgumentException(string.Empty));

            ValidationHelper.ValidateEmptyString(identification, true, $"{baseErrorMessage} La IDENTIFICACIÓN es obligatoria");
            ValidationHelper.ValidateEmptyString(name, true, $"{baseErrorMessage} El NOMBRE es obligatorio");
            ValidationHelper.ValidateEmptyString(lastName, true, $"{baseErrorMessage} El APELLIDO es obligatorio");
            ValidationHelper.ValidateEmptyString(phone, true, $"{baseErrorMessage} El TELÉFONO es obligatorio");
            ValidationHelper.ValidateEmptyString(adrress, true, $"{baseErrorMessage} La DIRECCIÓN es obligatoria");
        }
    }
}
