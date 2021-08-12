using Application.Contracts.Response;
using Domain.Entities;
using Infrastructure.Integrations.Mappers;
using Infrastructure.Integrations.Typicode.Contracts.Response.v1;
using Infrastructure.Integrations.Typicode.Interfaces;
using RestSharp;
using System.Collections.Generic;

namespace Infrastructure.Integrations.Typicode
{
    public class TypicodeClient : IntegrationClient, ITypicodeClient
    {
        private readonly ITypicodeConfigurations _typicodeConfigurations;

        public TypicodeClient(IRestClient restClient, ITypicodeConfigurations typicodeConfigurations) : base(restClient)
        {
            _typicodeConfigurations = typicodeConfigurations;
        }

        public ServiceResponse<List<Album>> GetAlbums()
        {
            var request = new RestRequest {
                Method = Method.GET,
                Resource = "/albums",
            };

            request.AddHeader("Content-Type", "application/json");

            try {

                var albums = new List<Album>();

                var response = ExecuteRequest<List<TypicodeAlbumResponse>>(_typicodeConfigurations.BaseUrl, request);

                response.ForEach(data => albums.Add(IntegrationsMappers.ToAlbum(data)));

                return ServiceResponse<List<Album>>.Success(albums);
            }
            catch (System.Exception ex) {

                return ServiceResponse<List<Album>>.Fail(ex.Message);
            }

        }

        public ServiceResponse<List<Album>> GetAlbumsByUserId(int userId)
        {
            var request = new RestRequest {
                Method = Method.GET,
                Resource = "/albums",
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("userId", userId, ParameterType.QueryString);

            try {

                var albums = new List<Album>();

                var response = ExecuteRequest<List<TypicodeAlbumResponse>>(_typicodeConfigurations.BaseUrl, request);

                response.ForEach(data => albums.Add(IntegrationsMappers.ToAlbum(data)));

                return ServiceResponse<List<Album>>.Success(albums);
            }
            catch (System.Exception ex) {

                return ServiceResponse<List<Album>>.Fail(ex.Message);
            }
        }

        public ServiceResponse<List<Photo>> GetPhotos(int albumId)
        {
            var request = new RestRequest {
                Method = Method.GET,
                Resource = "/photos",
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("albumId", albumId, ParameterType.QueryString);

            try {

                var photos = new List<Photo>();

                var response = ExecuteRequest<List<TypicodePhotoResponse>>(_typicodeConfigurations.BaseUrl, request);

                response.ForEach(data => photos.Add(IntegrationsMappers.ToPhoto(data)));

                return ServiceResponse<List<Photo>>.Success(photos);
            }
            catch (System.Exception ex) {

                return ServiceResponse<List<Photo>>.Fail(ex.Message);
            }
        }


    }
}
