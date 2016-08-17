/// <reference path="..\..\typings\app.d.ts" />

var app = angular.module("MindSauceDemo");

class PatientService implements IPatientService {

    constructor(private $http: ng.IHttpService, private $q: ng.IQService) {
    }

    addPatient(patient: Patient): ng.IPromise<any> {
        var dfd = this.$q.defer();

        var url = "http://localhost:63739/api/patients/add";

        this.$http.post(url, patient)
            .then((x: any) => {
                console.log(x);
                dfd.resolve();
            }, (response: any) => {
                if (response.status == 409) {
                    dfd.reject('Patient already registered');
                }
                else if (response.status == 422) {
                    dfd.reject(response.data);
                }
            });

        return dfd.promise;
    }

    public getPatients() {
        var dfd = this.$q.defer();

        var url = "http://localhost:63739/api/patients/registered";

        this.$http.get(url).then((response: ng.IHttpPromiseCallbackArg<ApiResponse<Patient[]>>) => {
            dfd.resolve(response.data.result);
        }, function (reason: any) {
            dfd.reject(reason);
        });

        return dfd.promise;
    }
}

app.service('patientService', PatientService);
