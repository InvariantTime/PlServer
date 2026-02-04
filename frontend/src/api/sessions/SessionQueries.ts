import { SessionLobbyInfo } from "./SessionLobbyInfo";

const getSessionListUrl = "http://localhost:5000/api/sessions/all";


export const getSessionList = async () => {
    
    const query: RequestInit = {
      'method': "GET",
      credentials: "include"
    }
  
    try
    {
        const result = await fetch(getSessionListUrl, query);

        if (result.ok) {
            const json = await result.json();
            return json as SessionLobbyInfo[];
        }
    }
    finally
    {
        return [];
    }
}