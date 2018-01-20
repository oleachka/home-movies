import { Injectable, Inject } from '@angular/core';
import { IGenre } from '../models/genrets';
import { Http } from '@angular/http';
import { Observable } from 'rxjs';
import { IMovie } from '../models/movie';



@Injectable()
export class MovieDataService {    
    _http: Http;
    _baseUrl: string;
    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this._baseUrl = baseUrl;
        this._http = http;
    }

    getAllMoviesAsync() : Observable<IMovie[]>{
        return this._http.get(this._baseUrl + 'api/movies/movies').map(result => {
            return result.json() as IMovie[];
        }, error => console.error(error));
    }

    addMoviesAsync(movie: IMovie): Observable<any> {
        return this._http.post(this._baseUrl + 'api/movies/addmovie', movie)
            .map(result => { }, error => console.error(error));
    }
    updateMoviesAsync(movie: IMovie): Observable<any> {
        return this._http.post(this._baseUrl + 'api/movies/updatemovie', movie)
            .map(result => { }, error => console.error(error));
    }

    getGenresAsync(): Observable<IGenre[]> {
        return this._http.get(this._baseUrl + 'api/movies/genres').map(result => {
            return result.json() as IGenre[];
        }, error => console.error(error));
    }
}