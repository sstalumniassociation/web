/* tslint:disable */
/* eslint-disable */
// Generated by Microsoft Kiota
// @ts-ignore
import { createStatusFromDiscriminatorValue, createUserFromDiscriminatorValue, type Status, type User } from '../../../../models/index.js';
// @ts-ignore
import { type BaseRequestBuilder, type Parsable, type ParsableFactory, type RequestConfiguration, type RequestInformation, type RequestsMetadata } from '@microsoft/kiota-abstractions';

/**
 * Builds and executes requests for operations under /v1/users/{id}/bind
 */
export interface BindRequestBuilder extends BaseRequestBuilder<BindRequestBuilder> {
    /**
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {Promise<User>}
     * @throws {Status} error when the service returns a 4XX or 5XX status code
     */
     post(requestConfiguration?: RequestConfiguration<object> | undefined) : Promise<User | undefined>;
    /**
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {RequestInformation}
     */
     toPostRequestInformation(requestConfiguration?: RequestConfiguration<object> | undefined) : RequestInformation;
}
/**
 * Uri template for the request builder.
 */
export const BindRequestBuilderUriTemplate = "{+baseurl}/v1/users/{id}/bind";
/**
 * Metadata for all the requests in the request builder.
 */
export const BindRequestBuilderRequestsMetadata: RequestsMetadata = {
    post: {
        uriTemplate: BindRequestBuilderUriTemplate,
        responseBodyContentType: "application/json",
        errorMappings: {
            XXX: createStatusFromDiscriminatorValue as ParsableFactory<Parsable>,
        },
        adapterMethodName: "send",
        responseBodyFactory:  createUserFromDiscriminatorValue,
    },
};
/* tslint:enable */
/* eslint-enable */