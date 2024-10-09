"use client"

import ListenSettings from "./components/ListenSettings";
import ServerSettings from "./components/ServerSettings";
import SpotifySettings from "./components/SpotifySettings";

export default function Home() {
	return (
		<section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10 w-full">
            <p>Configure</p>
			<div className="flex flex-col w-full items-center space-y-32">
				<ServerSettings/>
				<ListenSettings/>
				<SpotifySettings/>
			</div>
		</section>
	);
}