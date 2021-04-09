
//------------Filter Users NOT in Subject--------------->
$(document).ready(function () {
    $('#filter-name').keyup(function () {
        var name = $(this).val();
        var subjectId = $('#subjectId').val()
        console.log(subjectId)
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

//-------------------------Paging----------------------------->
//Go to specified page
function paging(pageNumber) {
    var set = document.getElementById("set").value;
    console.log(set)
    //var sortId = e.options[e.selectedIndex].text;
    //var productTypeId = $('#productTypeId').attr('data-value');

    $.ajax({
        type: 'GET',
        url: "/schoolclasses/index",
        data: { "pageNumber": pageNumber, "set": set /*, "productTypeId": productTypeId, "sortBy": sortId */ },
        success: function (result) {
            $("body").html(result);
        }
    });
}

