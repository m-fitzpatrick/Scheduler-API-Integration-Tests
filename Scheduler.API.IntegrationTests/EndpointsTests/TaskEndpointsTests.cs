using System;
using System.Collections.Generic;
using NUnit.Framework;
using Scheduler.API.IntegrationTests.Models;

namespace Scheduler.API.IntegrationTests.EndpointsTests
{
    class TaskEndpointsTests
    {
    }

    [TestFixture]
    public class Tasks : GivenAnApiClientHelper
    {
        private const string Prefix = "api/task";
        private const string GetIdRouteTemplate = "/id/{description}";
        private const string CrudTemplate = "/{id}";

        private readonly string _description = Guid.NewGuid().ToString();
        private readonly string _updatedDescription = Guid.NewGuid().ToString();
        private TaskViewModel _taskViewModel;
        private TaskViewModel _addResult;
        private TaskViewModel _getResult;
        private TaskViewModel _updateModel;
        private TaskViewModel _updateResult;

        [Test]
        public void CrudTest()
        {
            // Create an add model
            _taskViewModel = new TaskViewModel
                             {
                                 Description = _description
                             };
            // add the task
            _addResult = ApiClientHelper.PostResult<TaskViewModel, TaskViewModel>(Prefix, _taskViewModel);
            // assert that model and result are equal
            Assert.IsNotNull(_addResult);
            Assert.AreEqual(_addResult.Description, _taskViewModel.Description);

            // Get the task id from the description
            int taskId = ApiClientHelper.GetResult<int>(Prefix + GetIdRouteTemplate.Replace("{description}", _addResult.Description));
            Assert.AreEqual(_addResult.Id, taskId);
            // Get the task id from the result
            _getResult = ApiClientHelper.GetResult<TaskViewModel>(Prefix + CrudTemplate.Replace("{id}", taskId.ToString()));

            // Assert that the result and the add result are equal 
            Assert.IsNotNull(_getResult);
            Assert.AreEqual(_getResult.Description, _taskViewModel.Description);
            Assert.AreEqual(_getResult.Id, taskId);

            // create update model with new description
            _updateModel = new TaskViewModel
                           {
                               Description = _updatedDescription
                           };
            Assert.AreNotEqual(_addResult.Description, _updateModel.Description);

            // Update the task
            _updateResult = ApiClientHelper.PostResult<TaskViewModel, TaskViewModel>(Prefix + CrudTemplate.Replace("{id}", taskId.ToString()), _updateModel);
            Assert.IsNotNull(_updateResult);
            Assert.AreEqual(_updateModel.Description, _updateResult.Description);
            Assert.AreEqual(_updateResult.Id, taskId);

            // Get the task again
            _getResult = ApiClientHelper.GetResult<TaskViewModel>(Prefix + CrudTemplate.Replace("{id}", taskId.ToString()));
            // description should be new value
            Assert.AreEqual(_getResult.Description, _updatedDescription);
            Assert.AreEqual(_getResult.Id, taskId);
            // delete 
            ApiClientHelper.Delete(Prefix + CrudTemplate.Replace("{id}", taskId.ToString()));

            Assert.Throws<AggregateException>(() =>

                                              {
                                                  // this will throw, response is 404
                                                  _getResult = ApiClientHelper.GetResult<TaskViewModel>(Prefix + CrudTemplate.Replace("{id}", taskId.ToString()));
                                              });
            Assert.Pass("The task tests passed!!");
        }

        [Test]
        public void TasksCanBeRetrieved()
        {
            var tasks = ApiClientHelper.GetResult<IEnumerable<TaskViewModel>>(Prefix);
            Assert.IsNotNull(tasks);
        }
    }
}
