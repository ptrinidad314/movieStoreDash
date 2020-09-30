// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function displayActors(filmId) {
    //alert(filmId);

    $.post('home/getActors', { filmId: filmId }, function (result) {
        //alert('getActors');
        //array1.forEach(element => console.log(element));

        $('#filmActors').empty();

        result.forEach(elem => {
            console.log(elem)

            var liToAdd = '<li class="list-group-item" onClick="displayActorInfo(' + elem.actorId + ')">' + elem.firstName + ' ' + elem.lastName + '</li>';

            $('#filmActors').append(liToAdd);
        });
    });
}

function displayActorInfo(actorId) {
    $.post('home/getActorInfo', { actorId: actorId }, function (result) {
        console.log(result);

        $('#actorInfo').removeClass('d-none'); //.show();
        $('#actorInfo').addClass('d-flex');

        $('#actorId').val(result.actorID);
        $('#bio').html(result.bio);
        //$('#name').html(result.name);
        $('#firstName').val(result.firstname);
        $('#lastName').val(result.lastname);
        $('#actorImg').attr('src',result.imageUrl);
    });
}

//UpdateActorInfo(string bio, string firstName, string lastName, string actorId) 
function UpdateActorInfo() {
    var bio = $('#bio').html();
    var firstName = $('#firstName').val();
    var lastName = $('#lastName').val();
    var actorId = $('#actorId').val();

    $.post('home/UpdateActorInfo', { bio: bio, firstName: firstName, lastName: lastName, actorId: actorId }, function (result) {
        console.log('updated')
    });
}

function actorTabClick() {
    $('#actorTabPill').addClass('active');
    $('#actorTab').removeClass('d-none');
    $('#actorTab').addClass('d-flex');

    $('#moreInfoPill').removeClass('active');
    $('#moreInfo').removeClass('d-flex');
    $('#moreInfo').addClass('d-none');

    $('#yetMoreInfoPill').removeClass('active');
    $('#yetMoreInfo').removeClass('d-flex');
    $('#yetMoreInfo').addClass('d-none');
}

function moreInfoTabClick() {
    $('#actorTabPill').removeClass('active');
    $('#actorTab').removeClass('d-flex');
    $('#actorTab').addClass('d-none');

    $('#moreInfoPill').addClass('active');
    $('#moreInfo').removeClass('d-none');
    $('#moreInfo').addClass('d-flex');

    $('#yetMoreInfoPill').removeClass('active');
    $('#yetMoreInfo').removeClass('d-flex');
    $('#yetMoreInfo').addClass('d-none');
}

function yetMoreInfoTabClick() {
    $('#actorTabPill').removeClass('active');
    $('#actorTab').removeClass('d-flex');
    $('#actorTab').addClass('d-none');

    $('#moreInfoPill').removeClass('active');
    $('#moreInfo').removeClass('d-flex');
    $('#moreInfo').addClass('d-none');

    $('#yetMoreInfoPill').addClass('active');
    $('#yetMoreInfo').removeClass('d-none');
    $('#yetMoreInfo').addClass('d-flex');
}
