    $(document).ready(function () {
        $("#StateID").prop("disabled", false);

        $("#CountryID").change(function () {

            $("#StateID").html("");
            $("#StateID").prop("disabled", true);

            $("#CityID").html("");
            $("#CityID").prop("disabled", true);

            if ($("#CountryID").val() != "Select Country") {
                var CountryOptions = {};
                CountryOptions.url = "/AJAX/FillStates";
                CountryOptions.type = "POST";
                CountryOptions.data = JSON.stringify({ Country: $("#CountryID").val() });
                CountryOptions.datatype = "json";
                CountryOptions.contentType = "application/json";
                CountryOptions.success = function (StatesList) {
                    $("#StateID").html("");

                    $.each(StatesList, function (i, State) {
                        $("#StateID").append(
                            $('<option></option>').val(State.ID).html(State.Name));
                    });

                    $("#StateID").prop("disabled", false);
                };
                CountryOptions.error = function () { alert("Error in Getting States!!"); };
                $.ajax(CountryOptions);
            }
            else {
                $("#StateID").html("");
                $("#CityID").html("");

                $("#StateID").prop("disabled", true);
                $("#CityID").prop("disabled", true);
            }
        });
    });

    $(document).ready(function () {
        $("#CityID").prop("disabled", false);
        $("#StateID").change(function () {

            $("#CityID").html("");
            $("#CityID").prop("disabled", true);

            if ($("#StateID").val() != "Select State") {
                var StateOptions = {};
                StateOptions.url = "/AJAX/FillCities";
                StateOptions.type = "POST";
                StateOptions.data = JSON.stringify({ State: $("#StateID").val() });
                StateOptions.datatype = "json";
                StateOptions.contentType = "application/json";
                StateOptions.success = function (CitiesList) {
                    $("#CityID").html("");
                    $.each(CitiesList, function (i, City) {
                        $("#CityID").append(
                            $('<option></option>').val(City.ID).html(City.Name));
                    });

                    $("#CityID").prop("disabled", false);
                };
                StateOptions.error = function () { alert("Error in Getting Cities!!"); };
                $.ajax(StateOptions);
            }
            else {
                $("#CityID").html("");
                $("#CityID").prop("disabled", true);
            }
        });
    });