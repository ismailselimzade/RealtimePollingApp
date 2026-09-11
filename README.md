# Real-time Polling App

A real-time messaging server built with WebSockets.  
WebSocket-lər üzərində qurulmuş real vaxt rejimində mesajlaşma serveri.

---

## Features / Xüsusiyyətlər

- Accepts WebSocket connections and assigns each client a unique, thread-safe ID.  
  Hər client-ə unikal, thread-safe ID verilir və WebSocket bağlantısı qəbul edilir.

- Broadcasts incoming messages to all connected clients except the sender.  
  Gələn mesajlar göndərən istisna olmaqla bütün bağlı client-lərə yayımlanır.

- Handles disconnects gracefully, including unexpected drops.  
  Bağlantı kəsilmələrini, gözlənilməz kəsilmələr daxil olmaqla, düzgün idarə edir.

- Console client that can send and receive messages at the same time.  
  Mesajları eyni zamanda göndərə və qəbul edə bilən konsol client.

---

## What I Learned / Öyrəndiklərim

- How to work directly with `System.Net.WebSockets` to accept connections and handle messages.  
  Bağlantıları qəbul etmək və mesajları idarə etmək üçün birbaşa `System.Net.WebSockets` ilə necə işləmək olar.

- How to keep track of multiple connections safely with `ConcurrentDictionary`.  
  `ConcurrentDictionary` ilə çoxlu bağlantını thread-safe şəkildə necə izləmək olar.

- How to generate unique IDs safely when multiple clients connect at the same time.  
  Bir neçə client eyni anda qoşulanda unikal ID-ləri necə təhlükəsiz generasiya etmək olar.

- How to properly close a WebSocket connection instead of just dropping it.  
  WebSocket bağlantısını sadəcə kəsmək əvəzinə necə düzgün bağlamaq olar.

- How to stop one client's error from breaking the broadcast to everyone else.  
  Bir client-in xətasının digərlərinə broadcast-ı necə pozmamasını təmin etmək olar.

- How to run sending and receiving at the same time using `Task.WhenAll`.  
  `Task.WhenAll` ilə göndərmə və qəbul əməliyyatlarını eyni zamanda necə işlətmək olar.

---

## Tech Stack / Texnologiyalar

C#, ASP.NET Core, System.Net.WebSockets

---

## Endpoints

| Protocol | URL | Description |
|--------|-----|-------------|
| WebSocket | `/ws` | Connect and exchange real-time messages with other connected clients. / Qoşularaq digər bağlı client-lərlə real vaxt rejimində mesajlaşmaq. |

### Example Message / Nümunə Mesaj

```
Client 2 message: Hello everyone!
```

---

## Setup / Quraşdırma

1. Clone the repository.  
   Repozitorini kopyalayın.

2. Run the `RealtimePollingApp.Server` project.  
   `RealtimePollingApp.Server` proyektini işə salın.

    `dotnet run`

3. Update the WebSocket URL in `RealtimePollingApp.Client/Program.cs` to match the server's port.  
   `RealtimePollingApp.Client/Program.cs` faylındakı WebSocket URL-ini serverin portuna uyğun dəyişin.

4. Run one or more client instances.  
   Bir və ya bir neçə client instansını işə salın.

    `dotnet run`
