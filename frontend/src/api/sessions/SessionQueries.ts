import { Failure, Result, Success } from "../utils/Result";

const getSessionListUrl = "/api/sessions/all";
const createSessionUrl = "/api/sessions";

export type SessionCreationRequest = {
    name: string
}

export type SessionLobbyInfo = {
    name: string,
    maxUserCount: number,
    userCount: number,
    hostName: string,
    id: string
}

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

export const createSession = async (request: SessionCreationRequest) : Promise<Result<string>> => {

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
            const value = await result.json();
            return Success(value);
        }

        const value = await result.json();

        return Failure(value.error);
    }
    catch
    {
        return Failure("Unable to create session");
    }
}