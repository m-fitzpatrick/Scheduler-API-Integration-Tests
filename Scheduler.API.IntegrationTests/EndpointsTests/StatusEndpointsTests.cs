using System;
using System.Collections.Generic;
using NUnit.Framework;
using Scheduler.API.IntegrationTests.Models;

namespace Scheduler.API.IntegrationTests.EndpointsTests
{
    class StatusEndpointsTests
    {
    }

    [TestFixture]
    public class Statuses : GivenAnApiClientHelper
    {
        private const string Prefix = "api/status";
        private const string GetIdRouteTemplate = "/id/{description}";
        private const string CrudTemplate = "/{id}";

        private readonly string _description = Guid.NewGuid().ToString();
        private readonly string _updatedDescription = Guid.NewGuid().ToString();
        private readonly StatusType _statusType = StatusType.PartTime;
        private readonly StatusType _updatedStatusType = StatusType.FullTime;
        private PostStatusViewModel _postStatusViewModel;
        private StatusViewModel _addResult;
        private StatusViewModel _getResult;
        private PostStatusViewModel _updateModel;
        private StatusViewModel _updateResult;

        [Test]
        public void CrudTest()
        {
            // Create an add model
            _postStatusViewModel = new PostStatusViewModel
            {
                Description = _description,
                Type = _statusType,
                IsBenefitEligible = true,
                IsHolidayEligible = true
            };
            // add the status
            _addResult = ApiClientHelper.PostResult<PostStatusViewModel, StatusViewModel>(Prefix, _postStatusViewModel);
            // assert that model and result are equal
            Assert.IsNotNull(_addResult);
            Assert.AreEqual(_addResult.Description, _postStatusViewModel.Description);
            Assert.AreEqual(_addResult.IsBenefitEligible, _postStatusViewModel.IsBenefitEligible);
            Assert.AreEqual(_addResult.IsHolidayEligible, _postStatusViewModel.IsHolidayEligible);
            Assert.AreEqual(_addResult.Type, _postStatusViewModel.Type);

            // Get the status id from the description
            int statusId = ApiClientHelper.GetResult<int>(Prefix + GetIdRouteTemplate.Replace("{description}", _addResult.Description));
            Assert.AreEqual(_addResult.Id, statusId);
            // Get the status from the result
            _getResult = ApiClientHelper.GetResult<StatusViewModel>(Prefix + CrudTemplate.Replace("{id}", statusId.ToString()));

            // Assert that the result and the add result are equal 
            Assert.IsNotNull(_getResult);
            Assert.AreEqual(_getResult.Description, _description);
            Assert.AreEqual(_getResult.Type, _statusType);
            Assert.AreEqual(_getResult.Id, statusId);
            Assert.AreEqual(_getResult.IsBenefitEligible, true);
            Assert.AreEqual(_getResult.IsHolidayEligible, true);

            // create update model with new description
            _updateModel = new PostStatusViewModel
                           {
                               Description = _updatedDescription,
                               Type = _updatedStatusType,
                               IsBenefitEligible = false,
                               IsHolidayEligible = false
                           };

            Assert.AreNotEqual(_addResult.Description, _updateModel.Description);

            // Update the status
            _updateResult = ApiClientHelper.PostResult<PostStatusViewModel, StatusViewModel>(Prefix + CrudTemplate.Replace("{id}", statusId.ToString()), _updateModel);
            Assert.IsNotNull(_updateResult);
            Assert.AreEqual(_updateModel.Description, _updateResult.Description);
            Assert.AreEqual(_updateModel.Type, _updateResult.Type);
            Assert.AreEqual(_updateModel.IsBenefitEligible, _updateResult.IsBenefitEligible);
            Assert.AreEqual(_updateModel.IsHolidayEligible, _updateResult.IsHolidayEligible);
            Assert.AreEqual(_updateResult.Id, statusId);

            // Get the status again
            _getResult = ApiClientHelper.GetResult<StatusViewModel>(Prefix + CrudTemplate.Replace("{id}", statusId.ToString()));
            // description should be new value
            Assert.AreEqual(_getResult.Description, _updatedDescription);
            Assert.AreEqual(_getResult.Type, _updatedStatusType);
            Assert.AreEqual(_getResult.IsHolidayEligible, false);
            Assert.AreEqual(_getResult.IsBenefitEligible, false);
            Assert.AreEqual(_getResult.Id, statusId);

            // delete 
            ApiClientHelper.Delete(Prefix + CrudTemplate.Replace("{id}", statusId.ToString()));

            Assert.Throws<AggregateException>(() =>

            {
                // this will throw, response is 404
                _getResult = ApiClientHelper.GetResult<StatusViewModel>(Prefix + CrudTemplate.Replace("{id}", statusId.ToString()));
            });

            Assert.Pass("The status tests passed!!");
        }

        [Test]
        public void StatusesCanBeRetrieved()
        {
            var statuses = ApiClientHelper.GetResult<GetStatusViewModel>(Prefix);
            Assert.IsNotNull(statuses);
        }
    }
}
