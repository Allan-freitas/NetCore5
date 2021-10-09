using Application.Data.Context;
using Application.Data.Repositories;
using Application.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Application.Tests.Infrastructure.Data
{
    public class RepositoryTest
    {
        private readonly MockRepository _repositoryMock;
        private readonly Mock<IRepository<User>> _repositoryUser;
        private readonly IList<User> UserList = new List<User>
        {
            User.Create("Noah", "Noah@gmail.com", "21293811"),
            User.Create("Dila", "Dila@gmail.com", "54215465"),
            User.Create("Darcy", "Darcy@gmail.com", "54879845"),
            User.Create("Arivaldo", "Arivaldo@gmail.com", "85455489"),
            User.Create("Giovanna", "Giovanna@gmail.com", "54897545"),
            User.Create("Sara", "Sara@gmail.com", "87864551"),
        };

        public RepositoryTest()
        {
            _repositoryMock = new MockRepository(MockBehavior.Strict);
            _repositoryUser = _repositoryMock.Create<IRepository<User>>();            
        }

        [Fact]     
        [Trait("Backend", "Repository")]
        public void Insert_Object_In_DataBase()
        {
            const string Nome = "Allan";
            const string Senha = "21293811";
            const string Email = "freitasallan@gmail.com";
            User user = User.Create(Nome, Senha, Email);

            _repositoryUser.Setup(x => x.Insert(It.Is<User>(args => args == user))).Returns(User.Create(Nome, Senha, Email)).Verifiable();
            var resultadoEsperado = _repositoryUser.Object.Insert(user);

            Assert.NotNull(resultadoEsperado);
        }

        [Fact]
        [Trait("Backend", "Repository")]
        public void Not_Insert_In_DataBase_If_Password_Is_Null()
        {
            const string Nome = "Allan";
            const string Senha = null;
            const string Email = "freitasallan@gmail.com";
            User user = User.Create(Nome, Senha, Email);
            
            _repositoryUser.Setup(x => x.Insert(It.Is<User>(args => args == user))).Throws(new Exception("a senha não pode estar vazia")).Verifiable();
            void act() => _repositoryUser.Object.Insert(user);

            Assert.Throws<Exception>(act);
        }

        [Fact]
        [Trait("Backend", "Repository")]
        public void Can_We_Insert_User()
        {            
            User user = User.Create("Benjamin", "nimajneb", "benj@live.com");
            _repositoryUser.Setup(f => f.Table).Returns(UserList.AsQueryable());

            int numberOfUsers = _repositoryUser.Object.Table.Count();
            Assert.Equal(6, numberOfUsers);

            _repositoryUser.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(u => UserList.Add(u)).Returns(user);
            _repositoryUser.Object.Insert(user);

            numberOfUsers = _repositoryUser.Object.Table.Count();
            Assert.Equal(7, numberOfUsers);
        }
    }
}
