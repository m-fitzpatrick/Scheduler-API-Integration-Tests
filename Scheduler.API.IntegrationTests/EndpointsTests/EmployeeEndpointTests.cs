using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Scheduler.API.IntegrationTests.Models;

namespace Scheduler.API.IntegrationTests.EndpointsTests
{
    class EmployeesEndpointTests
    {
    }

    [TestFixture]
    public class Employees : GivenAnApiClientHelper
    {
        private const string Prefix = "api/employee";
        private const string GetIdRouteTemplate = "/id/{employeeId}";
        private const string CrudTemplate = "/{id}";

        private PostAddEmployeeViewModel _employeeViewModel;
        private GetEmployeeViewModel _addResult;
        private GetEmployeeViewModel _getResult;
        private PostEmployeeViewModel _updateModel;
        private GetEmployeeViewModel _updateResult;

        // address
        private readonly string _address1 = "123 Elm St";
        private readonly string _updatedAddress = "1400 Pennsylvania Ave";
        private readonly string _address2 = "Apt A";
        private readonly string _updatedAddress2 = string.Empty;
        private readonly string _city = "Huntington Beach";
        private readonly string _updatedCity = "Washington";
        private readonly string _state = "VA";
        private readonly string _updatedState = "DC";
        private readonly string _postalCode = "99992";
        private readonly string _updatedPostalCode = "12345";
        // employee
        private readonly string _cellPhone = "6145553636";
        private readonly string _updatedCellPhone = "6363555416";
        private readonly RoleName _role = RoleName.Schedulee;
        private readonly bool _isSupervisor = false;
        private readonly bool _isScheduler = false;
        private readonly string _firstName = "Bob";
        private readonly string _updatedFirstName = "Robert";
        private readonly string _lastName = "Jennings";
        private readonly string _updatedLastName = "Jenning Jr.";
        private readonly string _homePhone = "6145551234";
        private readonly string _updatedHomePhone = "4321555416";
        private readonly string _userName = "bJennings4";
        private readonly string _password = "structureM@p";
        private readonly DateTime _hireDate = DateTime.Now.Date;
        private readonly string _employeeId = "employeeId1";
        private readonly string _updatedEmployeeId = "employeeId2";

        [Test]
        public void CrudTest()
        {
            // title 
            IList<TitleViewModel> titles = ApiClientHelper.GetResult<IEnumerable<TitleViewModel>>("api/title").ToList();
            GetStatusViewModel statuses = ApiClientHelper.GetResult<GetStatusViewModel>("api/status");
            // Create an Add Model
            _employeeViewModel = new PostAddEmployeeViewModel
                                 {
                                     Address = new AddressViewModel
                                               {
                                                   Address1 = _address1,
                                                   Address2 = _address2,
                                                   City = _city,
                                                   State = _state,
                                                   ZipCode = _postalCode
                                               },
                                     CellPhone = _cellPhone,
                                     EmployeeId = _employeeId,
                                     FirstName = _firstName,
                                     LastName = _lastName,
                                     HireDate = _hireDate,
                                     HomePhone = _homePhone,
                                     IsScheduler = _isScheduler,
                                     IsSupervisor = _isSupervisor,
                                     Role = _role,
                                     Status = statuses.Statuses.First().Id,
                                     TitleId = titles.First().Id,
                                     Username = _userName,
                                     Password = _password,
                                     ConfirmPassword = _password
                                 };

            _addResult = ApiClientHelper.PostResult<PostAddEmployeeViewModel, GetEmployeeViewModel>(Prefix, _employeeViewModel);
            Assert.IsNotNull(_addResult);

            Guid userId = ApiClientHelper.GetResult<Guid>(Prefix + GetIdRouteTemplate.Replace("{employeeId}", _employeeId));

            Assert.AreNotEqual(userId, Guid.Empty);

            _getResult = ApiClientHelper.GetResult<GetEmployeeViewModel>(Prefix + CrudTemplate.Replace("{id}", userId.ToString()));

            Assert.IsNotNull(_getResult);
            Assert.IsNotNull(_getResult.Address);
            Assert.AreEqual(_getResult.Address.Address1, _address1);
            Assert.AreEqual(_getResult.Address.Address2, _address2);
            Assert.AreEqual(_getResult.Address.City, _city);
            Assert.AreEqual(_getResult.Address.State, _state);
            Assert.AreEqual(_getResult.Address.ZipCode, _postalCode);
            Assert.AreEqual(_getResult.CellPhone, _cellPhone);
            Assert.AreEqual(_getResult.EmployeeId, _employeeId);
            Assert.AreEqual(_getResult.FirstName, _firstName);
            Assert.AreEqual(_getResult.HireDate, _hireDate);
            Assert.AreEqual(_getResult.HomePhone, _homePhone);
            Assert.AreEqual(_getResult.Id, userId);
            Assert.AreEqual(_getResult.LastName, _lastName);
            Assert.AreEqual(_getResult.Title, titles.First().Description);
            Assert.AreEqual(_getResult.Roles.First(), _role.GetDescription());
            Assert.AreEqual(_getResult.Username, _userName);
            Assert.AreEqual(_getResult.Status, statuses.Statuses.First().Description);

            //created updated model
            _updateModel = new PostEmployeeViewModel
                           {
                               Address = new AddressViewModel
                                         {
                                             Address1 = _updatedAddress,
                                             Address2 = _updatedAddress2,
                                             City = _updatedCity,
                                             State = _updatedState,
                                             ZipCode = _updatedPostalCode
                                         },
                               CellPhone = _updatedCellPhone,
                               EmployeeId = _updatedEmployeeId,
                               FirstName = _updatedFirstName,
                               LastName = _updatedLastName,
                               HomePhone = _updatedHomePhone,
                               HireDate = _hireDate
                           };

            _updateResult = ApiClientHelper.PostResult<PostEmployeeViewModel, GetEmployeeViewModel>(Prefix + CrudTemplate.Replace("{id}", userId.ToString()), _updateModel);
            Assert.IsNotNull(_updateResult);

            _getResult = ApiClientHelper.GetResult<GetEmployeeViewModel>(Prefix + CrudTemplate.Replace("{id}", userId.ToString()));
            Assert.IsNotNull(_getResult);
            Assert.IsNotNull(_getResult.Address);
            Assert.AreEqual(_getResult.Address.Address1, _updatedAddress);
            Assert.AreEqual(_getResult.Address.Address2, _updatedAddress2);
            Assert.AreEqual(_getResult.Address.City, _updatedCity);
            Assert.AreEqual(_getResult.Address.State, _updatedState);
            Assert.AreEqual(_getResult.Address.ZipCode, _updatedPostalCode);
            Assert.AreEqual(_getResult.CellPhone, _updatedCellPhone);
            Assert.AreEqual(_getResult.EmployeeId, _updatedEmployeeId);
            Assert.AreEqual(_getResult.FirstName, _updatedFirstName);
            Assert.AreEqual(_getResult.HomePhone, _updatedHomePhone);
            Assert.AreEqual(_getResult.Id, userId);
            Assert.AreEqual(_getResult.LastName, _updatedLastName);

            _updateModel = new PostEmployeeViewModel
            {
                Address = new AddressViewModel
                {
                    Address1 = _updatedAddress,
                    Address2 = _updatedAddress2,
                    City = _updatedCity,
                    State = _updatedState,
                    ZipCode = _updatedPostalCode
                },
                CellPhone = _updatedCellPhone,
                EmployeeId = _updatedEmployeeId,
                FirstName = _updatedFirstName,
                LastName = _updatedLastName,
                HomePhone = _updatedHomePhone,
                HireDate = _hireDate
            };

            Guid userId2 = ApiClientHelper.GetResult<Guid>(Prefix + GetIdRouteTemplate.Replace("{employeeId}", _updatedEmployeeId));
            Assert.AreEqual(userId, userId2);

            Assert.Pass("Employee Tests Pass");
        }

        [Test]
        public void UpdateTestAfterCrudTest() // run the test above first
        {
            Guid userId = ApiClientHelper.GetResult<Guid>(Prefix + GetIdRouteTemplate.Replace("{employeeId}", _updatedEmployeeId));

            _updateModel = new PostEmployeeViewModel
            {
                Address = new AddressViewModel
                {
                    Address1 = _address1,
                    Address2 = _address2,
                    City = _city,
                    State = _state,
                    ZipCode = _postalCode
                },
                CellPhone = _cellPhone,
                EmployeeId = _employeeId,
                FirstName = _firstName,
                LastName = _lastName,
                HomePhone = _homePhone,
                HireDate = _hireDate
            };

            _updateResult = ApiClientHelper.PostResult<PostEmployeeViewModel, GetEmployeeViewModel>(Prefix + CrudTemplate.Replace("{id}", userId.ToString()), _updateModel);
            Assert.IsNotNull(_updateResult);

            _getResult = ApiClientHelper.GetResult<GetEmployeeViewModel>(Prefix + CrudTemplate.Replace("{id}", userId.ToString()));
            Assert.IsNotNull(_getResult);
            Assert.IsNotNull(_getResult.Address);
            Assert.AreEqual(_getResult.Address.Address1, _address1);
            Assert.AreEqual(_getResult.Address.Address2, _address2);
            Assert.AreEqual(_getResult.Address.City, _city);
            Assert.AreEqual(_getResult.Address.State, _state);
            Assert.AreEqual(_getResult.Address.ZipCode, _postalCode);
            Assert.AreEqual(_getResult.CellPhone, _cellPhone);
            Assert.AreEqual(_getResult.EmployeeId, _employeeId);
            Assert.AreEqual(_getResult.FirstName, _firstName);
            Assert.AreEqual(_getResult.HomePhone, _homePhone);
            Assert.AreEqual(_getResult.Id, userId);
            Assert.AreEqual(_getResult.LastName, _lastName);
        }
    }
}
