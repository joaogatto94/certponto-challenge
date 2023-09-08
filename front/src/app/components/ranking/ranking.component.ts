import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppService } from 'src/app/services/app.service';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-ranking',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.scss']
})
export class RankingComponent {
  get restaurants() {
    return this.appService.restaurants;
  }

  get maxVotes$(): BehaviorSubject<number> {
    return this.appService.maxVotes$;
  }

  constructor(private appService: AppService) {
  }

}
