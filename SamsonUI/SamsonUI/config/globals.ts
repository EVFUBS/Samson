import React, { createContext, useState } from "react";

export type SamsonTokenContextType = {
    token: string; 
    setToken: React.Dispatch<React.SetStateAction<string>>; 
}

export const SamsonTokenContext = createContext<SamsonTokenContextType | undefined>(undefined);
  