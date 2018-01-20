export interface IMovie {
    id: number;
    title: string;
    genre: number;
    genreName: string;
    rating: number;      
    actorList: string;
}

export class Movie implements IMovie
{
    id: number;
    title: string;
    genre: number;
    genreName: string;
    rating: number;
    actorList: string;

    constructor() {
        this.id = -1;
        this.title = "";       
        this.rating = -1;
        this.actorList = "";
    }
}

export class IMDBMovie extends Movie {
    plot: string;    
}