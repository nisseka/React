function GoToURL(url)
{
	document.location = url;
}

function GetPeopleList(clearMessage = true)
{
    $.get("/Ajax/GetPeopleList", null, function (data) {
        $("#OutputElement").html(data);
    });

    if (clearMessage)
        SetMessage("&nbsp;");
}

function GetPerson()
{
    var personIndexValue = document.getElementById('PersonIndexInput').value - 1;

    if (personIndexValue < 0)
    {
        personIndexValue = 0;
    }

    $.post("/Ajax/GetPersonByIndex", { personIndex: personIndexValue }, function (data) {
        $("#OutputElement").html(data);
    });

    SetMessage("&nbsp;");
}

function DeletePerson()
{
    var personIndexValue = document.getElementById('PersonIndexInput').value - 1;

    if (personIndexValue < 0)
    {
        personIndexValue = 0;
    }

    $.post("/Ajax/DeletePersonByIndex", { personIndex: personIndexValue }, function (data) { })
        .done(function ()
        {
            SetMessage("Successfully deleted person.");

            var personIndexInput = document.getElementById('PersonIndexInput');

            if (personIndexInput.max > 0)
            {
                personIndexInput.max--;
            }

            if (personIndexInput.value >= personIndexInput.max)
            {
                personIndexInput.value = personIndexInput.max;
            }

            GetPeopleList(false);
        })
        .fail(function ()
        {
            SetMessage("Could not delete person.");
        });

}

function SetMessage(Message)
{
    document.getElementById('Message').innerHTML = Message;
}