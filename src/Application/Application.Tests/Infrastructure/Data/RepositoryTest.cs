using Application.Data.Repositories;
using Application.Domain.Models.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Infrastructure.Data
{
    public class RepositoryTest
    {
        private readonly MockRepository _repositoryMock;
        private readonly Mock<IRepository<User>> _repositoryUser;
        private readonly List<User> UserList = new()
        {
            new User.UserBuilder().AddId(1).AddEmail("Dila@gmail.com").AddUsername("Dila").AddPassword("5468765").Build(),
            new User.UserBuilder().AddId(2).AddEmail("Darci@gmail.com").AddUsername("Darci").AddPassword("32545645").Build(),
            new User.UserBuilder().AddId(3).AddEmail("sara@gmail.com").AddUsername("Sara").AddPassword("56464654").Build(),
            new User.UserBuilder().AddId(4).AddEmail("Andres@gmail.com").AddUsername("Andres").AddPassword("21293811").Build(),
            new User.UserBuilder().AddId(5).AddEmail("Allan@gmail.com").AddUsername("Allan").AddPassword("89854654").Build(),
            new User.UserBuilder().AddId(6).AddEmail("Sebas@gmail.com").AddUsername("Sebastian").AddPassword("45648868").Build(),
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
            User user = new User.UserBuilder().AddPassword(Senha).AddUsername(Nome).AddId(34).AddEmail(Email).Build();

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
            User user = new User.UserBuilder().AddPassword(Senha).AddUsername(Nome).AddId(34).AddEmail(Email).Build();

            _repositoryUser.Setup(x => x.Insert(It.Is<User>(args => args == user))).Throws(new Exception("a senha não pode estar vazia")).Verifiable();
            void act() => _repositoryUser.Object.Insert(user);

            Assert.Throws<Exception>(act);
        }

        [Fact]
        [Trait("Backend", "Repository")]
        public void Can_We_Insert_User()
        {
            User user = new User.UserBuilder().AddPassword("876546687").AddUsername("Benjamin").AddId(34).AddEmail("Benjamin@gmail.com").Build();
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
                new User.UserBuilder().AddId(156).AddEmail("Sebastian@gmail.com").AddUsername("Sebas").AddPassword("6487").Build(),
                new User.UserBuilder().AddId(89).AddEmail("Sebas@gmail.com").AddUsername("Sebastian").AddPassword("45648868").Build()
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

        [Fact]
        [Trait("Backend", "Repository")]
        public async void Can_We_Insert_Async()
        {
            User user = new User.UserBuilder().AddId(10).AddEmail("Sebas@gmail.com").AddUsername("Sebastian").AddPassword("45648868").Build();

            /* Setup mock */
            _repositoryUser.Setup(f => f.InsertAsync(It.IsAny<User>())).Callback<User>(u => UserList.Add(u)).Returns(Task.FromResult(0));
            _repositoryUser.Setup(f => f.GetById(It.IsAny<int>())).Returns((int i) => UserList.Where(x => x.Id == i).FirstOrDefault());

            await _repositoryUser.Object.InsertAsync(user);
            User otherUser = _repositoryUser.Object.GetById(10);

            Assert.NotNull(otherUser);
        }

        [Fact]
        [Trait("Backend", "Repository")]
        public void Can_We_Delete()
        {
            /* Setup mock */
            _repositoryUser.Setup(f => f.GetById(It.IsAny<int>())).Returns((int i) => UserList.Where(x => x.Id == i).FirstOrDefault());
            _repositoryUser.Setup(s => s.Delete(It.IsAny<User>())).Callback<User>(u => UserList.Remove(u));
            _repositoryUser.Setup(f => f.Table).Returns(UserList.AsQueryable());

            User user = _repositoryUser.Object.GetById(2);
            Assert.NotNull(user);

            _repositoryUser.Object.Delete(user);
            int numberOfUsers = _repositoryUser.Object.Table.Count();
            Assert.Equal(5, numberOfUsers);
        }

        [Fact]
        [Trait("Backend", "Repository")]
        public void Can_We_Update_Entity()
        {
            _repositoryUser.Setup(f => f.GetById(It.IsAny<int>())).Returns((int i) => UserList.Where(x => x.Id == i).FirstOrDefault());
            _repositoryUser.Setup(p => p.Update(It.IsAny<User>())).Callback((User target) => 
            {
                User original = UserList.Where(u => u.Id == target.Id).FirstOrDefault();
                User updateUser = new User.UserBuilder().AddEmail(target.Email).AddId(target.Id).AddPassword(target.Password).AddUsername(target.Username).Build();
                UserList.Remove(original);
                UserList.Add(updateUser);
            });

            User originalUser = _repositoryUser.Object.GetById(1);
            User toBeUpdated = new User.UserBuilder().AddEmail("Sakamto@gmail.com").AddId(originalUser.Id).AddPassword(originalUser.Password).AddUsername(originalUser.Username).Build();

            _repositoryUser.Object.Update(toBeUpdated);
            Assert.Equal("Sakamto@gmail.com", _repositoryUser.Object.GetById(1).Email);
        }
    }
}
