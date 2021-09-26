using Application.Data.Repositories;
using Application.Domain.Models.Users;
using Moq;
using System;
using Xunit;

namespace Application.Tests.Infrastructure.Data
{
    public class RepositoryTest
    {
        private readonly MockRepository _repositoryMock;
        private readonly Mock<IRepository<User>> _repositoryUser;

        public RepositoryTest()
        {
            _repositoryMock = new MockRepository(MockBehavior.Strict);
            _repositoryUser = _repositoryMock.Create<IRepository<User>>();
        }

        [Fact]
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
        public void Not_Insert_In_DataBase_If_Password_Is_Null()
        {
            const string Nome = "Allan";
            const string Senha = null;
            const string Email = "freitasallan@gmail.com";
            User user = User.Create(Nome, Senha, Email);
            
            _repositoryUser.Setup(x => x.Insert(It.Is<User>(args => args == user))).Throws(new Exception("a senha não pode estar vazia")).Verifiable();            
            Action act = () => _repositoryUser.Object.Insert(user);

            Assert.Throws<Exception>(act);
        }
    }
}
