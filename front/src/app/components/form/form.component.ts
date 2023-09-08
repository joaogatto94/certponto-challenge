import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { ApiService } from 'src/app/services/api.service';
import { AppService } from 'src/app/services/app.service';



@Component({
  selector: 'app-form',
  standalone: true,
  imports: [CommonModule, MatRadioModule, FormsModule, MatInputModule, MatButtonModule],
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent {
  voteDate: Date | undefined;
  restaurantId: number | undefined;
  identifier: string | undefined;
  loading = false;

  get restaurants() {
    return this.appService.restaurants;
  }

  get weekWinners() {
    return this.appService.weekWinners;
  }

  constructor(private apiService: ApiService, private appService: AppService) {
    this.apiService.getVoteDate()
      .then(date => this.voteDate = date);
  }

  async vote() {
    if (!this.restaurantId) {
      alert(`Selecione um restaurante`);
      return;
    }

    if (!this.identifier || !this.identifier.trim()) {
      alert(`Digite seu identificador`);
      return;
    }

    this.loading = true;
    try {
      await this.apiService.vote(this.identifier, this.restaurantId);
      this.appService.setVotes();
      this.identifier = undefined;
      this.restaurantId = undefined;
    } catch (error: any) {
      switch (error.error) {
        case `IsWinner`:
          alert(`Esse restaurante ja venceu essa semana`);
          break;
        case `HasVoted`:
          alert(`Voce ja votou hoje!`);
          break;
        default:
          alert(`Error`);
          break;
      }
    }
    this.loading = false;
  }
}
