(function ()
{
    var app = angular.module("gsreport", []);
    //Michael Chodolak
    var ReportController = function ($scope, $http) {

        //Go to a3.chodolak.com's api with port 80
        var host = "a3.chodolak.com";
        var port = "80";

        //Puts in the correct url with token and api
        var baseURL = "http://" + host + ":" + port + "/";
        var loginURL = baseURL + "Token";
        var apiURL = baseURL + "api/Report/";

        //Preforms the login
        $scope.performLogin = function ()
        {

            var data = "&grant_type=password&" + "username=" + $scope.username + "&password=" + $scope.password;

            $http({
                method: 'POST',
                url: loginURL,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                data: $.param({grant_type: "password", username: $scope.username, password: $scope.password})
            })
                .success(function (response) {
                    $scope.report = response;
                    $scope.showlogin = false;
                    getMonthYear();
                })
                .error(function (response) {
                    $scope.loginerror = response.error_description;
                });
        }

        //Get the year
        var getMonthYear = function ()
        {
            $http.get(apiURL + "getyear")
            .success(function (response) {
                var json = JSON.stringify(response);
                var parser = JSON.parse(json);
                $scope.yearData = response;
                $scope.showMonthYear = true;
            });
        }

        //Loads the report
        $scope.loadReport = function ()
        {
            var month = $scope.selectedMonth;
            var yearID = $scope.selectedYear;
            if (typeof month === "undefined" || typeof yearID === "undefined") { return; }
            if (month.length >= 0 && yearID.length >= 0) {
                retrieveReport(month, yearID);
            }
            $scope.month = $("#selectMonth>option:selected").html();
            $scope.year = $("#selectYear>option:selected").html();


            var m_names = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September",
                "October", "November", "December");
            var d = new Date();
            $scope.currentdate = m_names[d.getMonth()] + " " + d.getDate() + "/" + d.getFullYear();
        }

        //Retrieves info for report
        var retrieveReport = function (monthNum, yearID)
        {
            $http.get(apiURL + "getReport?monthnum=" + monthNum + "&yearid=" + yearID)
                .success(function (response) {
                    //Gender
                    $scope.gendermale = response.genderMale;
                    $scope.genderfemale = response.genderFemale;
                    $scope.gendertrans = response.genderTrans;

                    //Age
                    $scope.age24_65 = response.age24_65;
                    $scope.age18_25 = response.age18_25;
                    $scope.age12_19 = response.age12_19;
                    $scope.age13 = response.age13;
                    $scope.age64 = response.age64;

                    //Status
                    $scope.statusopen = response.statusOpen;
                    $scope.statusclosed = response.statusClosed;
                    $scope.statusreopened = response.statusReopened;

                    //Program
                    $scope.programcrisis = response.programCrisis;
                    $scope.programcourt = response.programCourt;
                    $scope.programsmart = response.programSMART;
                    $scope.programdvu = response.programDVU;
                    $scope.programmcfd = response.programMCFD;

                    $scope.showreport = true;
                });
        }
        $scope.showlogin = true;
    };

    //Connects controller
    app.controller("ReportController", ["$scope", "$http", ReportController]);
}());
