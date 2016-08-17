/// <reference path="..\typings\app.d.ts" />

var app = angular.module("MindSauceDemo", ['ngComponentRouter']);

app.component('main', <ng.IComponentOptions>{
    template: '',
    $routeConfig: [
        { path: '/', name: 'App', component: 'app' },
        { path: '/mypatients', name: 'MyPatients', component: 'myPatients' },
        { path: '/addpatient', name: 'AddPatient', component: 'addPatient' },
    ]
});

app.value('$routerRootComponent', 'main');

app.component('app', {
    template: 'Welcome to Patient Administration',
});

app.component('myPatients', {
    templateUrl: 'app/patients/patients-list.component.html',
    controller: function (patientService: IPatientService) {
        this.patients = [];

        this.$routerOnActivate = () => patientService.getPatients()
            .then(x => {
                this.patients = x;
            }, reason => {
                console.log(reason);
            });
    }
});

app.component('addPatient', {
    templateUrl: 'app/patients/patient.component.html',
    bindings: {
        $router: '<',
    },
    controller: function (patientService: IPatientService) {
        this.add = function (patient) {

            if (this.patientForm.$valid) {
                patientService.addPatient(patient)
                    .then(x => {
                        this.$router.navigate(['MyPatients']);
                    }, errors => {
                        if (typeof errors === 'string') {
                            this.error = errors;
                        }
                        else {
                            this.validationErrors = errors
                        }
                    });
            }
        }
    }
})
