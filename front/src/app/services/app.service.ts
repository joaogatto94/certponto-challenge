import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  restaurants: {restaurantId: number, name: string, votes: number}[] = [];
  weekWinners: {[key: number]: boolean} = {};
  maxVotes$ = new BehaviorSubject<number>(10);

  constructor(
    private apiService: ApiService
  ) { }

  async init() {
    const weekWinners = await this.apiService.getWeekWinners();
    weekWinners.forEach(x => this.weekWinners[x] = true);
    this.restaurants = (await this.apiService.getRestaurants()).map(x => {
      x.votes = 0;
      return x;
    });
    this.setVotes();
  }

  async setVotes() {
    const votes = await this.apiService.getTodayVotes();
    this.maxVotes$.next(votes.length);
    this.restaurants = this.restaurants.map(r => {
      r.votes = votes.filter(v => v.restaurantId === r.restaurantId).length;
      return r;
    })
  }
}
