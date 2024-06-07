export type SiteConfig = typeof siteConfig;

export const siteConfig = {
	name: "Next.js + NextUI",
	description: "Make beautiful websites regardless of your design experience.",
	navItems: [
		{
			label: "Home",
			href: "/",
		},
		{
			label: "Talk",
			href: "/talk",
		},
		{
			label: "Configure",
			href: "/configure"
		},
		{
			label: "Demo",
			href: "/demo"
		}
	],
	navMenuItems: [
		{
			label: "Talk",
			href: "/talk",
		},
		{
			label: "Configure",
			href: "/configure",
		},
		{
			label: "Demo",
			href: "/demo"
		},
		{
			label: "Logout",
			href: "/logout",
		},
	],
	links: {},
};
