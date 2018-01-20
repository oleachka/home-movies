import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { IMovie } from '../../models/movie';
import { MovieDataService } from '../../services/movie-data.service';
import { Observable } from 'rxjs/Observable';


@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    pageTitle: string = "My Movies";
    movies: IMovie[];
    filteredMovies: IMovie[];

    _listFilter: string;
    get listFilter(): string {
        return this._listFilter;
    }
    set listFilter(value: string) {
        this._listFilter = value;
        this.filteredMovies = this.listFilter ? this.performFilter(this.listFilter) : this.movies;
    }

    constructor(private dataService: MovieDataService) {
        dataService.getAllMoviesAsync().subscribe(result => {
            this.filteredMovies = this.movies = result;                
            this.listFilter = "";
        });

    }
    onRatingClicked(rating: number, movie: IMovie) {       
        movie.rating = rating;
        this.dataService.updateMoviesAsync(movie).subscribe(result => {           
        }, error => console.error(error)); 
    }
    performFilter(filterBy: string): IMovie[] {
        filterBy = filterBy.toLocaleLowerCase();
        return this.movies.filter((movie: IMovie) =>
            movie.title.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
            movie.genreName.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
            movie.actorList.toLocaleLowerCase().indexOf(filterBy) !== -1
        );
    }
   
}
     

