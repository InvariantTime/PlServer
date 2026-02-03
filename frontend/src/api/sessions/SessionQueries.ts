import { SessionLobbyInfo } from "./SessionLobbyInfo";

const getSessionListUrl = "";


export const getSessionList = async () => {
    
    const query: RequestInit = {
      'method': "GET",
    }
  
    const result = await fetch(getSessionListUrl, query);
    
    if (result.ok) {
        const json = await result.json();
        return json as SessionLobbyInfo[];
    }

    return [];
  }