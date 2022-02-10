import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
    private hubConnection: signalR.HubConnection;
    
    constructor(private toastr: ToastrService) { }

    public subscribe = () => {
        const options = {
            accessTokenFactory: () => {
                return localStorage.getItem('token');
            },
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
        };

        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(environment.notificationsUrl + 'notifications', options)
            .build();

        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err));

        this.hubConnection.on('ReceiveNotificationCreatedUser', (data) => {
            console.log(data);
            this.toastr.success(`Welcome, ${data.firstName}, ${data.lastName}!!!`, "New Employee!");
        });

        this.hubConnection.on('ReceiveNotificationAssignedSurvey', (data) => {
            console.log(data);
            this.toastr.success(`You have been assigned to: ${data.surveyName}!!!`, "New Survey!");
        });
    }
}