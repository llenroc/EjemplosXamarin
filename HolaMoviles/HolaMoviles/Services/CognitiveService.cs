using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using System.IO;
using System.Linq;
using Microsoft.ProjectOxford.Vision.Contract;

namespace HolaMoviles
{
	public class CognitiveService
	{
		const string API_KEY = "90447e2668524ea0bad4d714c4446865";
		const string VISION_URL = "https://westus.api.cognitive.microsoft.com/vision/v1.0";

		public async Task<AnalysisResult> AnalyzeImage(byte[] mediaStream)
		{
			AnalysisResult result = null;

			try
			{
				if (CrossConnectivity.Current.IsConnected == false)
				{
					return null;
				}
				VisualFeature[] features = { VisualFeature.Tags, VisualFeature.Categories, VisualFeature.Description, VisualFeature.Color };
				var visionClient = new VisionServiceClient(API_KEY);

				result = await visionClient.AnalyzeImageAsync(new MemoryStream(mediaStream), features.ToList(), null);

				Debug.WriteLine(result.ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			return result;
		}

		public async Task<OcrResults> RecognizeTextAsync(byte[] mediaStream)
		{
			if (CrossConnectivity.Current.IsConnected == false)
			{
				return null;
			}

			var visionClient = new VisionServiceClient(API_KEY);

			var result = await visionClient.RecognizeTextAsync(new MemoryStream(mediaStream));

			Debug.WriteLine(result.ToString());

			return result;
		}
	}
}