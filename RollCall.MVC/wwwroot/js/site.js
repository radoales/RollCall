
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