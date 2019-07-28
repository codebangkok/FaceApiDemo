package main

import (
	"context"
	"fmt"
	"os"

	"github.com/Azure/azure-sdk-for-go/services/cognitiveservices/v1.0/face"
	"github.com/Azure/go-autorest/autorest"
)

func main() {
	client := face.NewClient("https://southeastasia.api.cognitive.microsoft.com")
	client.Authorizer = autorest.NewCognitiveServicesAuthorizer("b225200629ab4207afa72cbecbc4dcb0")

	returnFaceID := true
	returnFaceLandmarks := false
	returnFaceAttributes := []face.AttributeType{face.AttributeTypeAge, face.AttributeTypeGender, face.AttributeTypeSmile, face.AttributeTypeGlasses}
	var recognitionModel face.RecognitionModel
	returnRecognitionModel := false
	var detectionModel face.DetectionModel

	// var image face.ImageURL
	// url := "https://thethaiger.com/wp-content/uploads/2019/07/66829455_640821486424524_965362129626464256_n.jpg"
	// image.URL = &url
	// faces, err := client.DetectWithURL(context.Background(), image, &returnFaceID, &returnFaceLandmarks, returnFaceAttributes, recognitionModel, &returnRecognitionModel, detectionModel)

	file, err := os.Open("bond.jpg")
	if err != nil {
		fmt.Println(err.Error())
		return
	}
	faces, err := client.DetectWithStream(context.Background(), file, &returnFaceID, &returnFaceLandmarks, returnFaceAttributes, recognitionModel, &returnRecognitionModel, detectionModel)
	if err != nil {
		fmt.Println(err.Error())
		return
	}

	for _, face := range *faces.Value {
		fmt.Println(face.FaceID, *face.FaceRectangle.Left, *face.FaceRectangle.Top, *face.FaceRectangle.Width, *face.FaceRectangle.Height)
		fmt.Println(face.FaceAttributes.Gender, *face.FaceAttributes.Age, *face.FaceAttributes.Smile, face.FaceAttributes.Glasses)
	}
}
