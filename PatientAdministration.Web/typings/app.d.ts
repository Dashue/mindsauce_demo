/// <reference path="index.d.ts" />

interface Patient {
    firstname: string;
    surname: string;
    birthdate: Date;
    note: string
}

interface ApiResponse<T>{
    result: T;
}

interface IPatientService{
    getPatients() : ng.IPromise<Patient[]>;
    addPatient(patient : Patient) : ng.IPromise<any>;
}