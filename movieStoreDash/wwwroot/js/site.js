// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(document).ready(function () {



});






function displaySocialMedia() {

    var url = $('#actorSocialMediaURL').val();

    if (url === '') {
        alert('error, no social media url');
    } else {
        window.open(url, "MsgWindow", "width=700,height=700");
    }

}

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
        //console.log(result);

        $('#actorInfo').removeClass('d-none'); //.show();
        $('#actorInfo').addClass('d-flex');

        $('#actorId').val(result.actorID);
        //$('#bio').html(result.bio);
       
        $('#firstName').val(result.firstname);
        $('#lastName').val(result.lastname);
        $('#actorImg').attr('src', result.imageUrl);

        //socialMediaURL
        $('#socialMediaURL').val(result.socialMediaURL);

        //$('#mediaIF').attr('src', result.socialMediaURL);

        $('#actorSocialMediaURL').val(result.socialMediaURL);

        //alert(result.autoOpenURL);
        $('#mediaIF').attr('src', result.socialMediaURL);

        if (result.autoOpenURL) {
            //alert('open');
            //$('#mediaIF').attr('src', '');
            displaySocialMedia();

        }


        /*
         $.get(link, function (response){
            var html = response;
            var html_src = 'data:text/html;charset=utf-8,' + html;
            $("#iframeId").attr("src" , html_src);
            });
         
         
         */

        /*
        else {
            //alert('do not open');
            $('#mediaIF').attr('src', result.socialMediaURL);
        }*/



        //var testURL = 'https://www.phoenixnewtimes.com/music/september-2020-new-songs-arizona-phoenix-musicians-11497873';
        //var testURL = 'https://www.instagram.com/p/CFujbiBBSeh/';
        //$('#mediaIF').attr('src', testURL);
     

        //console.log('the iframe test');
        //console.log($('#mediaIF'));

        //var iframe = document.getElementById('iframe');
        //console.log(iframe);
        //var theHtml = iframe.contentWindow.document.body.innerHTML;

        
        //console.log(theHtml);

    });
}

//UpdateActorInfo(string bio, string firstName, string lastName, string actorId) 
function UpdateActorInfo() {
    var bio = $('#bio').html();
    var firstName = $('#firstName').val().trim();
    var lastName = $('#lastName').val().trim();
    var actorId = $('#actorId').val().trim();

    var socialMediaURL = $('#socialMediaURL').val().trim();

    $.post('home/UpdateActorInfo', { bio: bio, firstName: firstName, lastName: lastName, actorId: actorId, socialMediaURL: socialMediaURL }, function (result) {
        alert('updated')
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
