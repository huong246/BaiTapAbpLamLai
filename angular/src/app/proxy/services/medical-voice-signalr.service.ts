import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject, BehaviorSubject } from 'rxjs';
export interface FieldData {
  field_name: string;
  content: string;
}

export interface MedicalFieldsUpdate {
  fields: FieldData[];
  timestamp: string;
  source: string;
}

export interface StatusUpdate {
  status: 'processing' | 'analyzing' | 'completed' | 'error';
  message?: string;
  timestamp: string;
}

@Injectable({
  providedIn: 'root',
})

export class MedicalVoiceSignalRService {
  private hubConnection!: signalR.HubConnection;
  private connectionStatus = new BehaviorSubject<string>('disconnected');

  private medicalFieldsSubject = new Subject<MedicalFieldsUpdate>();
  private fieldUpdateSubject = new Subject<FieldData>();
  private statusUpdateSubject = new Subject<StatusUpdate>();

  public medicalFields$ = this.medicalFieldsSubject.asObservable();
  public fieldUpdate$ = this.fieldUpdateSubject.asObservable();
  public statusUpdate$ = this.statusUpdateSubject.asObservable();
  public connectionStatus$ = this.connectionStatus.asObservable();

  constructor() {}

  public startConnection(): Promise<void> {
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl('https://localhost:44362/hubs/medical-voice',
      {
        skipNegotiation: false,
        transport: signalR.HttpTransportType.WebSockets | signalR.HttpTransportType.LongPolling
      }).withAutomaticReconnect([0, 2000, 5000, 10000]) // Retry delays
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.registerHandlers();

    return this.hubConnection.start().then(() => {
      console.log('Connected');
      this.connectionStatus.next('connected');
    }).catch(err => {
      console.log('SignalR connection error:', err);
      this.connectionStatus.next('error');
      throw err;
    });
  }

  private registerHandlers(): void {
    this.hubConnection.on('ReceiveMedicalData', (data: MedicalFieldsUpdate) =>
    {
      console.log('receive medical fields:', data);
      this.medicalFieldsSubject.next(data);
    });

    this.hubConnection.on('ReceiveFieldUpdate', (data: { field: FieldData; timestamp: string }) => {
      console.log('📥 Received field update:', data);
      this.fieldUpdateSubject.next(data.field);
    });
    this.hubConnection.on('ReceiveStatus', (data: StatusUpdate) => {
      console.log('📊 Status update:', data);
      this.statusUpdateSubject.next(data);
    });
    this.hubConnection.on('Connected', (data: any) => {
      console.log('✅ Connected to hub:', data);
    });

    // Handler: Reconnecting
    this.hubConnection.onreconnecting(error => {
      console.warn('⚠️ Reconnecting...', error);
      this.connectionStatus.next('reconnecting');
    });

    // Handler: Reconnected
    this.hubConnection.onreconnected(connectionId => {
      console.log('✅ Reconnected:', connectionId);
      this.connectionStatus.next('connected');
    });

    // Handler: Close
    this.hubConnection.onclose(error => {
      console.error('❌ Connection closed:', error);
      this.connectionStatus.next('disconnected');
    });
  }

  public stopConnection(): Promise<void>{
    if(this.hubConnection) {
      return this.hubConnection.stop().then(() => {
        console.log('SignalR disconnected');
        this.connectionStatus.next('disconnected');
      });
    }
    return Promise.resolve();
  }

  public subscribeToUpDates(): Promise<void> {
    if(this.hubConnection.state === signalR.HubConnectionState.Connected) {
      return this.hubConnection.invoke('SubscribeToMedicalUpdates');
    }
    return Promise.reject('Not connected');
  }

  public isConnected(): boolean {
    return this.hubConnection?.state === signalR.HubConnectionState.Connected;
  }

  public getConnectionId(): string | null {
    return this.hubConnection?.connectionId ?? null;
  }


}

