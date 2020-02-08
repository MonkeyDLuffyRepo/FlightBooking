import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { extend } from 'webdriver-js-extender';
import { FlightBaseModel, PlaneDetail, AirportBaseModel, AirportDetail } from '../models/models';


@Injectable({
  providedIn: 'root'
})
export class FlightService {
    _baseURL: string;
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { this._baseURL = baseUrl+'api/flight' }

    public getAllFlights(): Observable<FlightBaseModel[]> {
        const fullUrl = this._baseURL + '/get-all-flight';

        return this.http.get<FlightBaseModel[]>(
            fullUrl
        );
    }

    public getAllPlanes(): Observable<PlaneDetail[]> {
        const fullUrl = this._baseURL + '/get-all-planes';

        return this.http.get<PlaneDetail[]>(
            fullUrl
        );
    }
    public getAllAirports(): Observable<AirportBaseModel[]> {
        const fullUrl = this._baseURL + '/get-all-airport';

        return this.http.get<PlaneDetail[]>(
            fullUrl
        );
    }

    public getFlightById(id: number): Observable<FlightBaseModel> {
        const fullUrl = this._baseURL + '/get-flight-by-id/' + id;

        return this.http.get<FlightBaseModel>(
            fullUrl
        );
    }

    public addFlight(newFlight: FlightBaseModel): Observable<number> {
        const fullUrl = this._baseURL + '/add-flight';

        return this.http.post<number>(
            fullUrl,
            newFlight
        );
    }

    public updateContact(updatedFlight: FlightBaseModel): Observable<void> {
        const fullUrl = this._baseURL + '/update-flight';

        return this.http.put<void>(
            fullUrl,
            updatedFlight
        );
    }

    public deleteFlight(id: number): Observable<void> {
        const fullUrl = this._baseURL + '/delete-flight/' + id;

        return this.http.delete<void>(
            fullUrl
        );
    }

}
