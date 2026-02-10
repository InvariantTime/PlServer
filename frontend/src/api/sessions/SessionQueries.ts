import { SessionCreationRequest } from "./SessionCreationRequest";
import { SessionLobbyInfo } from "./SessionLobbyInfo";

const getSessionListUrl = "/api/sessions/all";
const createSessionUrl = "/api/sessions";


export const getSessionList = async () : Promise<SessionLobbyInfo[]> => {
    
    const query: RequestInit = {
      'method': "GET",
      credentials: "include"
    }
  
    try
    {
        const result = await fetch(getSessionListUrl, query);

        if (result.ok) {
            const json = await result.json();
            return json;
        }
    }
    catch
    {
        return [];
    }

    return [];
}

export const createSession = async (request: SessionCreationRequest) => {

        const query: RequestInit = {

      'method': "POST",
      credentials: "include",
      headers: {
        "content-type": "application/json"
      },
      body: JSON.stringify(request)
    }
  
    try
    {
        const result = await fetch(createSessionUrl, query);

        if (result.ok) {
            return 1;
        }
    }
    finally
    {
        return 0;
    }
}