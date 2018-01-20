import { Component, Inject, Input, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { IMDBMovie } from '../../models/movie';


@Component({
    selector: 'imdb',
    templateUrl: './imdb.component.html'
})
export class IMDBComponent implements OnInit {
    pageTitle: string = "IMDB Movie Info";

    movieTitle: string;
    notFound: boolean = false;
    movie: IMDBMovie = new IMDBMovie();
    constructor(private route: ActivatedRoute, private http: Http)  {     
        //http://www.omdbapi.com/?s=kill&apikey=fab416ed
        
    }
    ngOnInit() {
         this.route.params.subscribe(params => {             
             this.movieTitle = params['movieTitle']; 

             let url = "http://www.omdbapi.com/?t=" + this.movieTitle + "&apikey=fab416ed";
             this.http.get(url)
                 .subscribe(result => {                    
                     var response = result.json();
                     if (response.Response == "True") {
                         this.movie = new IMDBMovie();
                         this.movie.title = response.Title;
                         this.movie.rating = response.imdbRating;
                         this.movie.genreName = response.Genre;
                         this.movie.actorList = response.Actors;                         
                         this.movie.plot = response.Plot;
                     }
                     else {
                         this.notFound = true;
                     }
                 }
                 ), error => console.error(error);
        });
    }
}