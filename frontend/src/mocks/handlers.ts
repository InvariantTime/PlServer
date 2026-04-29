import { http, HttpResponse } from 'msw'

export const handlers = [
    // GET /api/sessions/all
    http.get('/api/sessions/all', () => {
        return HttpResponse.json([
            { 
                id: '1', 
                name: 'Test Session', 
                userCount: 2, 
                maxUserCount: 5,
                hostName: 'TestUser'
            }
        ])
    }),

    http.get('/api/users/verify', () => {
        return HttpResponse.json({name: "user"});
    })
]