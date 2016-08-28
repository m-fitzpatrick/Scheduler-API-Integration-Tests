using System;
using System.Collections.Generic;
using NUnit.Framework;
using Scheduler.API.IntegrationTests.Models;

namespace Scheduler.API.IntegrationTests.EndpointsTests
{
    class JobCodeEndpointsTests
    {
    }

    [TestFixture]
    public class JobCodes : GivenAnApiClientHelper
    {
        private const string Prefix = "api/jobcode";
        private const string GetIdRouteTemplate = "/id/{code}";
        private const string CrudTemplate = "/{id}";

        private readonly string _description = Guid.NewGuid().ToString();
        private readonly string _updatedDescription = Guid.NewGuid().ToString();

        // Job Code codes are always in upper case
        private readonly string _code = Guid.NewGuid().ToString().ToUpper();
        private readonly string _updatedCode = Guid.NewGuid().ToString().ToUpper();
        private readonly string _altCode = Guid.NewGuid().ToString();
        private readonly string _updatedAltCode = Guid.NewGuid().ToString();
        private readonly DateTime _lastDate = DateTime.UtcNow;
        private JobCodeViewModel _jobCodeViewModel;
        private JobCodeViewModel _addResult;
        private JobCodeViewModel _getResult;
        private JobCodeViewModel _updateModel;
        private JobCodeViewModel _updateResult;

        [Test]
        public void CrudTest()
        {
            // Create an add model
            _jobCodeViewModel = new JobCodeViewModel
            {
                Code = _code,
                Description = _description,
                AltCode = _altCode
            };
            // add the job code
            _addResult = ApiClientHelper.PostResult<JobCodeViewModel, JobCodeViewModel>(Prefix, _jobCodeViewModel);
            // assert that model and result are equal
            Assert.IsNotNull(_addResult);
            Assert.AreEqual(_addResult.Code, _jobCodeViewModel.Code);
            Assert.AreEqual(_addResult.Description, _jobCodeViewModel.Description);
            Assert.AreEqual(_addResult.AltCode, _jobCodeViewModel.AltCode);
            Assert.AreEqual(_addResult.LastDate, _jobCodeViewModel.LastDate);

            // Get the job code id from the code
            int jobCodeId = ApiClientHelper.GetResult<int>(Prefix + GetIdRouteTemplate.Replace("{code}", _addResult.Code));
            Assert.AreEqual(_addResult.Id, jobCodeId);
            // Get the job code id from the result
            _getResult = ApiClientHelper.GetResult<JobCodeViewModel>(Prefix + CrudTemplate.Replace("{id}", jobCodeId.ToString()));

            // Assert that the result and the add result are equal 
            Assert.IsNotNull(_getResult);
            Assert.AreEqual(_getResult.Code, _jobCodeViewModel.Code);
            Assert.AreEqual(_getResult.Description, _jobCodeViewModel.Description);
            Assert.AreEqual(_getResult.AltCode, _jobCodeViewModel.AltCode);
            Assert.AreEqual(_getResult.LastDate, _jobCodeViewModel.LastDate);
            Assert.AreEqual(_getResult.Id, jobCodeId);

            // create update model with new description
            _updateModel = new JobCodeViewModel
            {
                Code = _updatedCode,
                Description = _updatedDescription,
                AltCode = _updatedAltCode,
                LastDate = _lastDate
            };

            Assert.AreNotEqual(_addResult.Code, _updateModel.Code);
            Assert.AreNotEqual(_addResult.Description, _updateModel.Description);

            // Update the job code
            _updateResult = ApiClientHelper.PostResult<JobCodeViewModel, JobCodeViewModel>(Prefix + CrudTemplate.Replace("{id}", jobCodeId.ToString()), _updateModel);
            Assert.IsNotNull(_updateResult);
            Assert.AreEqual(_updateModel.Code, _updateResult.Code);
            Assert.AreEqual(_updateModel.Description, _updateResult.Description);
            Assert.AreEqual(_updateModel.AltCode, _updateResult.AltCode);
            Assert.AreEqual(_updateModel.LastDate, _updateResult.LastDate);
            Assert.AreEqual(_updateResult.Id, jobCodeId);

            // Get the job code again
            _getResult = ApiClientHelper.GetResult<JobCodeViewModel>(Prefix + CrudTemplate.Replace("{id}", jobCodeId.ToString()));
            // description should be new value
            Assert.AreEqual(_getResult.Code, _updatedCode);
            Assert.AreEqual(_getResult.Description, _updatedDescription);
            Assert.AreEqual(_getResult.AltCode, _updatedAltCode);
            Assert.AreEqual(_getResult.LastDate.Value.Date, _lastDate.Date); // because of JSON serialization we lose some fractional second accuracy
            Assert.AreEqual(_getResult.Id, jobCodeId);
            
            // no delete on job code
            Assert.Pass("The job code tests passed!!");
        }

        [Test]
        public void JobCodesCanBeRetrieved()
        {
            var jobCodes = ApiClientHelper.GetResult<IEnumerable<JobCodeViewModel>>(Prefix);
            Assert.IsNotNull(jobCodes);
        }
    }
}
