import type { MetaFunction } from "@remix-run/node";
import { useMutation } from "@tanstack/react-query";
import { useState } from "react";
import Navbar from "~/components/UI/navbar";
import IResponse from "~/interfaces/IResponse";
import { shortenUrl } from "~/services/ServiceShortenURL";

export const meta: MetaFunction = () => {
  return [
    { title: "URL Shortener" },
    { name: "description", content: "Welcome to Remix (SPA Mode)!" },
  ];
};


export default function Index() {
  const [url, setUrl] = useState<string>("");
  const [shortenedUrl, setShortenedUrl] = useState<string>("");

  const mutation = useMutation<IResponse, Error, string>({
    mutationFn: shortenUrl,
    onSuccess: (data: IResponse) => {
      setShortenedUrl(data.shortenedURL);
    },
  });
  const handleShortenUrl = () => {
    mutation.mutate(url);
  };
  return (
    <>
      <Navbar />
<div className="min-h-screen flex flex-col items-center justify-center bg-gray-900 text-white p-4">
      <div className="text-center mb-8">
        <h1 className="text-4xl font-bold mb-2">URL Shortener</h1>
        <p className="text-gray-400">Simplify your links</p>
      </div>
      <main className="w-full max-w-lg bg-gray-800 p-6 rounded-lg shadow-lg">
        <div className="mb-4">
          <label htmlFor="url" className="block text-gray-300 mb-2">Enter your URL</label>
          <input 
            type="text" 
            id="url" 
            className="w-full p-3 rounded bg-gray-700 text-white border border-gray-600 focus:outline-none focus:border-blue-500" 
            value={url} 
            onChange={(e) => setUrl(e.target.value)} 
            placeholder="https://example.com" 
          />
        </div>
        <button 
          onClick={handleShortenUrl} 
          className="w-full p-3 bg-blue-600 hover:bg-blue-700 rounded text-white font-bold transition duration-300"
        >
          {mutation.isPending ? 'Shortening...' : 'Shorten URL'}
        </button>
        {mutation.isError && (
          <div className="mt-4 p-3 bg-red-700 rounded text-center">
            <p className="text-white">An error occurred: {mutation.error.message}</p>
          </div>
        )}
        {shortenedUrl && (
          <div className="mt-4 p-3 bg-gray-700 rounded text-center">
            <p className="text-gray-300 mb-1">Shortened URL:</p>
            <a 
              href={shortenedUrl} 
              className="text-blue-400 hover:underline" 
              target="_blank" 
              rel="noopener noreferrer"
            >
              {shortenedUrl}
            </a>
          </div>
        )}
      </main>
    </div>
    </>
  );
};
