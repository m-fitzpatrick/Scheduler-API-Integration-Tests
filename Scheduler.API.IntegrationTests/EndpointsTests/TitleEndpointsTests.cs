using System;
using System.Collections.Generic;
using NUnit.Framework;
using Scheduler.API.IntegrationTests.Models;

namespace Scheduler.API.IntegrationTests.EndpointsTests
{
    class TitleEndpointsTests
    {
    }
    
    [TestFixture]
    public class Titles : GivenAnApiClientHelper
    {
        private const string Prefix = "api/title";
        private const string GetIdRouteTemplate = "/id/{description}";
        private const string CrudTemplate = "/{id}";

        private readonly string _description = Guid.NewGuid().ToString();
        private readonly string _updatedDescription = Guid.NewGuid().ToString();
        private TitleViewModel _titleViewModel;
        private TitleViewModel _addResult;
        private TitleViewModel _getResult;
        private TitleViewModel _updateModel;
        private TitleViewModel _updateResult;

        [Test]
        public void CrudTest()
        {
            // Create an add model
            _titleViewModel = new TitleViewModel
                                            {
                                                Description = _description,
                                                IsExempt = false
                                            };
            // add the title
            _addResult = ApiClientHelper.PostResult<TitleViewModel, TitleViewModel>(Prefix, _titleViewModel);
            // assert that model and result are equal
            Assert.IsNotNull(_addResult);
            Assert.AreEqual(_addResult.Description, _titleViewModel.Description);
            Assert.AreEqual(_addResult.IsExempt, _titleViewModel.IsExempt);

            // Get the title id from the description
            int titleId = ApiClientHelper.GetResult<int>(Prefix + GetIdRouteTemplate.Replace("{description}", _addResult.Description));
            Assert.AreEqual(_addResult.Id, titleId);
            // Get the title id from the result
            _getResult = ApiClientHelper.GetResult<TitleViewModel>(Prefix + CrudTemplate.Replace("{id}", titleId.ToString()));

            // Assert that the result and the add result are equal 
            Assert.IsNotNull(_getResult);
            Assert.AreEqual(_getResult.Description, _titleViewModel.Description);
            Assert.AreEqual(_getResult.IsExempt, _titleViewModel.IsExempt);
            Assert.AreEqual(_getResult.Id, titleId);

            // create update model with new description
            _updateModel = new TitleViewModel
                           {
                               Description = _updatedDescription,
                               IsExempt = true
                           };
            Assert.AreNotEqual(_addResult.Description, _updateModel.Description);

            // Update the title
            _updateResult = ApiClientHelper.PostResult<TitleViewModel, TitleViewModel>(Prefix + CrudTemplate.Replace("{id}", titleId.ToString()), _updateModel);
            Assert.IsNotNull(_updateResult);
            Assert.AreEqual(_updateModel.Description, _updateResult.Description);
            Assert.AreEqual(_updateModel.IsExempt, _updateResult.IsExempt);
            Assert.AreEqual(_updateResult.Id, titleId);

            // Get the title again
            _getResult = ApiClientHelper.GetResult<TitleViewModel>(Prefix + CrudTemplate.Replace("{id}", titleId.ToString()));
            // description should be new value
            Assert.AreEqual(_getResult.Description, _updatedDescription);
            Assert.AreEqual(_getResult.Id, titleId);

            // delete 
            ApiClientHelper.Delete(Prefix + CrudTemplate.Replace("{id}", titleId.ToString()));

            Assert.Throws<AggregateException>(() =>

                                     {
                                         // this will throw, response is 404
                                         _getResult = ApiClientHelper.GetResult<TitleViewModel>(Prefix + CrudTemplate.Replace("{id}", titleId.ToString()));
                                     });                   
            Assert.Pass("The title tests passed!!");
        }

        [Test]
        public void TitlesCanBeRetrieved()
        {
            var titles = ApiClientHelper.GetResult<IEnumerable<TitleViewModel>>(Prefix);
            Assert.IsNotNull(titles);
        }
    }
}
