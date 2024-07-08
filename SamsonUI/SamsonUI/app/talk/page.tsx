"use client"

import { Button } from "@nextui-org/button";
import {Input, Textarea} from "@nextui-org/input";
import {Divider} from "@nextui-org/divider";
import {useState} from "react";

export default function Page() {
    const [talking, setTalking] = useState<boolean>();
    const [talkingColour, setTalkingColour] = useState<string>("default");
    const [talkingButtonMessage, setTalkingButtonMessage] = useState<string>("Press to start listening");
    
    const CaptureAudioData = async() => {
      const userMediaPromise = navigator.mediaDevices.getUserMedia({
        audio: true,
        video: false
      })
      const stream = await userMediaPromise;
      const mediaRecorder = new MediaRecorder(stream);
      const audioChunks: BlobPart[] = [];
  
      mediaRecorder.start();
  
      mediaRecorder.addEventListener("dataavailable", event => {
        audioChunks.push(event.data);
      });
  
      mediaRecorder.addEventListener("stop", () => {
        const audioBlob = new Blob(audioChunks, {
          type: 'audio/webm'
        });
      });
    }
    
    return (
        <section className={"flex flex-col gap-5"}>
          <div className={"flex flex-col gap-2"}>
            <h2>Try using samson with text commands</h2>
            <Input placeholder={"Enter command here"} />
            <Textarea placeholder={"Waiting for response..."} readOnly={true}/>
            <Button>Submit</Button>
          </div>
          <Divider />
          <div className={"flex flex-col gap-2"}>
            <h2>Try using samson with voice commands</h2>
            <Button color={talkingColour} 
                    onMouseEnter={() => {
                      if (talkingColour != "success") {
                        setTalkingColour("warning")
                      }
                    }} 
                    onMouseLeave={() => {
                      if (talkingColour != "success") {
                        setTalkingColour("default")
                      }
                    }}
                    onClick={() => {
                      
                      setTalking(!talking)
                      
                      if(talkingColour === "success"){
                        setTalkingColour("default")
                        setTalkingButtonMessage("Press to start listening")
                      } else {
                        setTalkingColour("success")
                        setTalkingButtonMessage("Press to stop listening")
                      }
                      
                    }}
            >{talkingButtonMessage}</Button>
          </div>
        </section>
    )
}
