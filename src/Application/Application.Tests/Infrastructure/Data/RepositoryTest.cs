using Application.Data.Repositories;
using Application.Domain.Models.Users;
using Moq;
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
        private readonly List<User> UserList = new()
        {
            User.Create(1, "Noah", "Noah@gmail.com", "21293811"),
            User.Create(2, "Dila", "Dila@gmail.com", "54215465"),
            User.Create(3, "Darcy", "Darcy@gmail.com", "54879845"),
            User.Create(4, "Arivaldo", "Arivaldo@gmail.com", "85455489"),
            User.Create(5, "Giovanna", "Giovanna@gmail.com", "54897545"),
            User.Create(6, "Sara", "Sara@gmail.com", "87864551"),
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
            User user = User.Create(11, Nome, Senha, Email);

            _repositoryUser.Setup(x => x.Insert(It.Is<User>(args => args == user))).Returns(user).Verifiable();
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
            User user = User.Create(10, Nome, Senha, Email);
            
            _repositoryUser.Setup(x => x.Insert(It.Is<User>(args => args == user))).Throws(new Exception("a senha não pode estar vazia")).Verifiable();
            void act() => _repositoryUser.Object.Insert(user);

            Assert.Throws<Exception>(act);
        }

        [Fact]
        [Trait("Backend", "Repository")]
        public void Can_We_Insert_User()
        {            
            User user = User.Create(9, "Benjamin", "nimajneb", "benj@live.com");
            _repositoryUser.Setup(f => f.Table).Returns(UserList.AsQueryable());

            int numberOfUsers = _repositoryUser.Object.Table.Count();
            Assert.Equal(6, numberOfUsers);

            _repositoryUser.Setup(x => x.Insert(It.IsAny<User>())).Callback<User>(u => UserList.Add(u)).Returns(user);
            _repositoryUser.Object.Insert(user);

            numberOfUsers = _repositoryUser.Object.Table.Count();
            Assert.Equal(7, numberOfUsers);
        }

        [Fact]
        [Trait("Backend", "Repository")]
        public void Can_We_Insert_Many()
        {
            List<User> users = new()
            {
                User.Create(7, "Edgar", "nimajneb", "Edgar@live.com"),
                User.Create(8, "Andres", "nimajneb", "Andres@live.com")
            };

            _repositoryUser.Setup(f => f.Table).Returns(UserList.AsQueryable());
            _repositoryUser.Setup(u => u.Insert(It.IsAny<IEnumerable<User>>())).Callback<IEnumerable<User>>(u => UserList.AddRange(u));

            int numberOfUsers = _repositoryUser.Object.Table.Count();
            Assert.Equal(6, numberOfUsers);

            _repositoryUser.Object.Insert(users);
            numberOfUsers = _repositoryUser.Object.Table.Count();

            Assert.Equal(8, numberOfUsers);
        }

        [Fact]
        [Trait("Backend", "Repository")]
        public void Can_We_Find_By_Id()
        {
            _repositoryUser.Setup(f => f.Table).Returns(UserList.AsQueryable());
            _repositoryUser.Setup(f => f.GetById(It.IsAny<int>())).Returns((int i) => UserList.Where(x => x.Id == i).FirstOrDefault());

            User user = _repositoryUser.Object.GetById(1);    
            
            Assert.NotNull(user);
            Assert.Equal(user.Username, _repositoryUser.Object.GetById(1).Username);
        }
    }
}
