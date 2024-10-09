"use client"

import {Button} from "@nextui-org/button";
import {Input, Textarea} from "@nextui-org/input";
import {Divider} from "@nextui-org/divider";
import {useState} from "react";
import {samsonServerClient} from "@/clients/Clients";

export default function Page() {
  const [actionRequestValue, setActionRequestValue] = useState<string>("");
  const [responseValue, setResponseValue] = useState<string>("");
  const [talking, setTalking] = useState<boolean>();
  const [talkingColour, setTalkingColour] = useState<string>("default");
  const [talkingButtonMessage, setTalkingButtonMessage] = useState<string>("Press to start listening");
  const [mediaRecorder, setMediaRecorder] = useState<MediaRecorder>();
  const audioChunks: BlobPart[] = [];

  const captureAudioData = async () => {
    const userMediaPromise = navigator.mediaDevices.getUserMedia({
      audio: true,
      video: false
    })
    const stream = await userMediaPromise;
    console.log("stream", stream);
    setMediaRecorder(new MediaRecorder(stream));

    if (mediaRecorder !== undefined) {
      mediaRecorder.start();

      mediaRecorder.addEventListener("dataavailable", event => {
        console.log(event);
        audioChunks.push(event.data);
      });

      mediaRecorder.addEventListener("stop", () => {
        console.log(audioChunks);
      })
    }
  }

  const cycleLoadingTexts = () => {
    const loadingTexts = ["Waiting for response.", "Waiting for response..", "Waiting for response..."];
    let textIndex = 0;

    return setInterval(() => {
      setResponseValue(loadingTexts[textIndex]);
      textIndex = (textIndex + 1) % loadingTexts.length;
    }, 400);
  };

  const sendActionRequestToSamson = async () => {
    const intervalId = cycleLoadingTexts();
    const response = await samsonServerClient.action(actionRequestValue);

    if (response.action == 1002) {
      const response = await samsonServerClient.question(actionRequestValue);
      clearInterval(intervalId);
          
      console.log(response)
      setResponseValue(response.text!);

      if (response.text == null) {
        setResponseValue("Something went wrong :(");
        return;
      }
    } else {
      clearInterval(intervalId);
      setResponseValue(JSON.stringify(response));
    }
  };

  const sendWakeRequestToSamson = async () => {
    if (audioChunks.length < 0) {
      const audioBlob = new Blob(audioChunks, {type: 'audio/wav'});
      const reader = new FileReader();

      reader.readAsDataURL(audioBlob);
      reader.onloadend = async () => {
        const base64String = reader.result?.toString().split(',')[1];
        const response = await fetch('https://localhost:44306/api/Wake', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/octet-stream',
          },
          body: base64String
        });
        const data = response.json();
        setResponseValue(data[Symbol.toStringTag])
      }
    }
  }

  return (
    <section className={"flex flex-col gap-5"}>
      <h2>Samson's Response:</h2>
      <Textarea value={responseValue} placeholder={"Waiting for response..."} readOnly={true}/>
      <Divider/>
      <div className={"flex flex-col gap-2"}>
        <h2>Try using samson with text commands</h2>
        <Input
          onChange={(e) => setActionRequestValue(e.target.value)}
          value={actionRequestValue}
          placeholder={"Enter command here"}
          onKeyDown={(e) => {
            if (e.key === 'Enter') {
              sendActionRequestToSamson();
            }
          }}
        />
        <Button onClick={sendActionRequestToSamson}>Submit</Button>
      </div>
      <div className={"flex flex-col gap-2"}>
        <h2>Try using samson with voice commands</h2>
        <Button onMouseEnter={() => {
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
                  if (talkingColour === "success") {
                    mediaRecorder?.stop();
                    setTalkingColour("default");
                    setTalkingButtonMessage("Press to start listening");
                    setMediaRecorder(undefined);
                  } else {
                    captureAudioData().then(_ => {
                      setTalkingColour("success");
                      setTalkingButtonMessage("Press to stop listening");
                    });
                  }
                }}
        >{talkingButtonMessage}</Button>
        <Button onClick={sendWakeRequestToSamson}>Submit</Button>
      </div>
    </section>
  )
}
