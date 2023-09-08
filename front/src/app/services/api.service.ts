import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly API_URL = 'https://localhost:7229';

  constructor(
    private httpClient: HttpClient
  ) { }

  getRestaurants(): Promise<any[]> {
    return firstValueFrom(this.httpClient.get(`${this.API_URL}/restaurants`)) as Promise<any[]>;
  }

  getTodayVotes(): Promise<any[]> {
    return firstValueFrom(this.httpClient.get(`${this.API_URL}/votes/today`)) as Promise<any[]>;
  }

  getVoteDate(): Promise<Date> {
    return firstValueFrom(this.httpClient.get(`${this.API_URL}/votes/vote-date`)) as Promise<Date>;
  }

  getWeekWinners(): Promise<number[]> {
    return firstValueFrom(this.httpClient.get(`${this.API_URL}/votes/week-winners`)) as Promise<number[]>;
  }

  vote(identifier: string, restaurantId: number) {
    return firstValueFrom(this.httpClient.post(`${this.API_URL}/votes`, {identifier, restaurantId}));
  }
}
