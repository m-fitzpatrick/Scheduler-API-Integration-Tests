using System;
using System.Collections.Generic;
using NUnit.Framework;
using Scheduler.API.IntegrationTests.Models;

namespace Scheduler.API.IntegrationTests.EndpointsTests
{
    class TermReasonEndpointsTests
    {
    }

    [TestFixture]
    public class TermReasons : GivenAnApiClientHelper
    {
        private const string Prefix = "api/termreason";
        private const string GetIdRouteTemplate = "/id/{description}";
        private const string CrudTemplate = "/{id}";

        private readonly string _description = Guid.NewGuid().ToString();
        private readonly string _updatedDescription = Guid.NewGuid().ToString();
        private TermReasonViewModel _termReasonViewModel;
        private TermReasonViewModel _addResult;
        private TermReasonViewModel _getResult;
        private TermReasonViewModel _updateModel;
        private TermReasonViewModel _updateResult;

        [Test]
        public void CrudTest()
        {
            // Create an add model
            _termReasonViewModel = new TermReasonViewModel
            {
                Description = _description
            };
            // add the term reason
            _addResult = ApiClientHelper.PostResult<TermReasonViewModel, TermReasonViewModel>(Prefix, _termReasonViewModel);
            // assert that model and result are equal
            Assert.IsNotNull(_addResult);
            Assert.AreEqual(_addResult.Description, _termReasonViewModel.Description);

            // Get the term reason id from the description
            int termReasonId = ApiClientHelper.GetResult<int>(Prefix + GetIdRouteTemplate.Replace("{description}", _addResult.Description));
            Assert.AreEqual(_addResult.Id, termReasonId);
            // Get the term reason id from the result
            _getResult = ApiClientHelper.GetResult<TermReasonViewModel>(Prefix + CrudTemplate.Replace("{id}", termReasonId.ToString()));

            // Assert that the result and the add result are equal 
            Assert.IsNotNull(_getResult);
            Assert.AreEqual(_getResult.Description, _termReasonViewModel.Description);
            Assert.AreEqual(_getResult.Id, termReasonId);

            // create update model with new description
            _updateModel = new TermReasonViewModel
            {
                Description = _updatedDescription
            };
            Assert.AreNotEqual(_addResult.Description, _updateModel.Description);

            // Update the term reason
            _updateResult = ApiClientHelper.PostResult<TermReasonViewModel, TermReasonViewModel>(Prefix + CrudTemplate.Replace("{id}", termReasonId.ToString()), _updateModel);
            Assert.IsNotNull(_updateResult);
            Assert.AreEqual(_updateModel.Description, _updateResult.Description);
            Assert.AreEqual(_updateResult.Id, termReasonId);

            // Get the term reason again
            _getResult = ApiClientHelper.GetResult<TermReasonViewModel>(Prefix + CrudTemplate.Replace("{id}", termReasonId.ToString()));
            // description should be new value
            Assert.AreEqual(_getResult.Description, _updatedDescription);
            Assert.AreEqual(_getResult.Id, termReasonId);
            // delete 
            ApiClientHelper.Delete(Prefix + CrudTemplate.Replace("{id}", termReasonId.ToString()));

            Assert.Throws<AggregateException>(() =>

            {
                // this will throw, response is 404
                _getResult = ApiClientHelper.GetResult<TermReasonViewModel>(Prefix + CrudTemplate.Replace("{id}", termReasonId.ToString()));
            });
            Assert.Pass("The term reason tests passed!!");
        }

        [Test]
        public void TermReasonsCanBeRetrieved()
        {
            var termReasons = ApiClientHelper.GetResult<IEnumerable<TermReasonViewModel>>(Prefix);
            Assert.IsNotNull(termReasons);
        }
    }
}
