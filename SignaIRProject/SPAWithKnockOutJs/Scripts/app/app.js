var taskListViewModel = {
    tasks: ko.observableArray(),
    canCreate:ko.observable(true)
};

var taskModel = function () {
    this.taskId = 0;
    this.taskName = ko.observable();
    this.description = ko.observable();
    this.finishTime = ko.observable();
    this.owner = ko.observable();
    this.state = ko.observable();
    this.fromJs = function (data) {
        this.taskId = data.taskId;
        this.taskName(data.taskName);
        this.description(data.description);
        this.finishTime(data.finishTime);
        this.owner(data.owner);
        this.state(data.state);
    };
};


function getAllTasks() {
    sendAjaxRequest('GET', function (data) {
        taskListViewModel.tasks.removeAll();
        for (var i = 0; i < data.length; i++) {
            taskListViewModel.tasks.push(data[i]);
        }
    }, 'GetAll', { 'state': 'all' });
}

function setTaskList(state) {
    console.log(state);
    sendAjaxRequest('GET', function (data) {
        taskListViewModel.tasks.removeAll();
        for (var i = 0; i < data.length; i++) {
            taskListViewModel.tasks.push(data[i]);
        }
    }, 'GetByState', { 'state': state });
}

function remove(item) {
    sendAjaxRequest("DELETE", function () {
        getAllTasks();
    }, item.taskId);
}

var task = new taskModel();
function handleCreateOrUpdate(item) {
    task.fromJs(item);
    initDatePicker();
    taskListViewModel.canCreate(false);
    $('#create').css('visibility', 'visible');
}

function handleBackClick() {
    taskListViewModel.canCreate(true);
    $('#create').css('visibility', 'hidden');
}

function handleSaveClick(item) {
    if (item.taskId == undefined) {
        sendAjaxRequest('POSt', function (newItem) {
            taskListViewModel.tasks.push(newItem);
        }, null, {
            taskName: item.taskName,
            description: item.description,
            finishTime: item.finishTime,
            owner: item.owner,
            state: item.state
        });
    } else {
        sendAjaxRequest('PUT', function () {
            getAllTasks();
        }, null, {
            taskId: item.taskId,
            taskName: item.taskName,
            description: item.description,
            finishTime: item.finishTime,
            owner: item.owner,
            state: item.state
        });
    }
    taskListViewModel.canCreate(true);
    $('#create').css('visibility', 'hidden');
}

//js简单封装ajax请求
function sendAjaxRequest(httpMethod, callback, url, reqData) {
    $.ajax('/api/tasks' + (url ? '/' + url : ''), {
        type: httpMethod,
        success: callback,
        data:reqData
    });
}

var initDatePicker = function () {
    $('#create .datepicker').datepicker({
        autoclose :true
    });
};

$('.nav').on('click', 'li', function () {
    $('.nav li active').removeClass('active');
    $(this).addClass('active');
});

$(document).ready(function () {
    getAllTasks();
    //使用knockoutJS绑定
    ko.applyBindings(taskListViewModel, $('#list').get(0));
    ko.applyBindings(task, $('#create').get(0));
});