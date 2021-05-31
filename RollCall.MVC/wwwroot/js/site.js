
//------------Filter Users NOT in Subject--------------->
$(document).ready(function () {
    $('#filter-name').keyup(function () {
        var name = $(this).val();
        var subjectId = $('#subjectId').val()

        $.ajax({
            type: 'GET',
            url: "/subjects/GetAdduserSubjectsVM",
            data: { "name": name, "subjectId": subjectId },
            success: function (result) {
                $('#user-subject-partial').html(result);
            }
        });
    });
})

//------------Filter Teacher's Students--------------->
$(document).ready(function () {
    $('#filter-teachers-students').keyup(function () {
        var name = $(this).val();
        var pageNumber = $('#page-number').val()
        $.ajax({
            type: 'GET',
            url: "/users/GetTeachersStudents",
            data: { "name": name, "pageNumber": pageNumber },
            success: function (result) {
                $('#teachers-students').html(result);
            }
        });
    });
})

//------------Filter SchoolClasses' Attendances--------------->
$(document).ready(function () {
    $('#filter-schoolClasses-attendances').keyup(function () {
        var searchString = $(this).val();
        var classId = $('#classId').val();

        $.ajax({
            type: 'GET',
            url: "/schoolClasses/GetSchoolClassAttendances",
            data: { "classId": classId, "searchString": searchString },
            success: function (result) {
                $('#schoolClasses-attendances').html(result);
            }
        });
    });
})



//------------Add User to Subject--------------->
function Add(element) {
    var subjectId = $('#subjectId').val()
    var userId = element.dataset.id
    $.ajax({
        type: 'POST',
        url: "/subjects/AddUserToSubject",
        data: { "userId": userId, "subjectId": subjectId },
        success: function (result) {
            $("body").html(result);
        }
    });
}

//------------Remove User from Subject--------------->
function Remove(element) {
    var subjectId = $('#subjectId').val()
    var userId = element.dataset.id
    $.ajax({
        type: 'POST',
        url: "/subjects/RemoveUserFromSubject",
        data: { "userId": userId, "subjectId": subjectId },
        success: function (result) {
            $("body").html(result);
        }
    });
}

//------------Select Upcoming or Passed Classes--------------->

function UpcomingClasses() {
    $.ajax({
        type: 'GET',
        url: "/schoolclasses/index",
        data: { "set": "upcoming" },
        success: function (result) {
            $("body").html(result);
        }
    });
}

function PassedClasses() {
    $.ajax({
        type: 'GET',
        url: "/schoolclasses/index",
        data: { "set": "passed" },
        success: function (result) {
            $("body").html(result);
        }
    });
}

function selectSchoolClassesSet() {
    var schoolClassesSet = $('#schoolClassesSet').val()
    $.ajax({
        type: 'GET',
        url: "/schoolclasses/index",
        data: { "schoolClassesSet": schoolClassesSet },
        success: function (result) {
            $("body").html(result);
        }
    });
}

//-------------------------Loading animation----------------------------->
function loadingOn() {
    document.getElementById("overlay").style.display = "block";
}

function loadingOff() {
    document.getElementById("overlay").style.display = "none";
}


//-------------------------Paging SchoolClasses----------------------------->
//Go to specified page
function pagingSchoolClasses(pageNumber) {
    var schoolClassesSet = document.getElementById("schoolClassesSet").value;
    console.log(schoolClassesSet)
    //var sortId = e.options[e.selectedIndex].text;
    //var productTypeId = $('#productTypeId').attr('data-value');

    $.ajax({
        type: 'GET',
        url: "/schoolclasses/index",
        data: { "pageNumber": pageNumber, "schoolClassesSet": schoolClassesSet /*, "productTypeId": productTypeId, "sortBy": sortId */ },
        beforeSend: function () {
            loadingOn();
        },
        success: function (result) {
            loadingOff();
            $("body").html(result);
        }
    });
}

//-------------------------Paging Users----------------------------->
//Go to specified page
function pagingUsers(pageNumber) {
    //var set = document.getElementById("set").value;
    //console.log(set)
    //var sortId = e.options[e.selectedIndex].text;
    //var productTypeId = $('#productTypeId').attr('data-value');
    var name = $('#filter-teachers-students').val()
    $.ajax({
        type: 'GET',
        url: "/users/index",
        data: { "pageNumber": pageNumber, "name": name /* "sortBy": sortId */ },
        success: function (result) {
            $("body").html(result);
        }
    });
}

//-------------------Color attendance percantage---------------------->

$(document).ready(function () {
    var mc = {
        '0-49': 'red',
        '50-69': 'orange',
        '70-100': 'green'
    };
    function between(x, min, max) {
        return x >= min && x <= max;
    }

    var dc;
    var first;
    var second;
    var th;

    $('p').each(function (index) {

        th = $(this);

        dc = parseInt($(this).attr('data-color'), 10);

        $.each(mc, function (name, value) {

            first = parseInt(name.split('-')[0], 10);
            second = parseInt(name.split('-')[1], 10);

            console.log(between(dc, first, second));

            if (between(dc, first, second)) {
                th.addClass(value);
            }
        });
    });
});