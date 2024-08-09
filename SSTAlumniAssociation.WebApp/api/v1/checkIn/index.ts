/* tslint:disable */
/* eslint-disable */
// Generated by Microsoft Kiota
// @ts-ignore
import { createCheckInFromDiscriminatorValue, createListCheckInsResponseFromDiscriminatorValue, createStatusFromDiscriminatorValue, serializeCheckIn, serializeCreateCheckInRequest, type CheckIn, type CreateCheckInRequest, type ListCheckInsResponse, type Status } from '../../models/index.js';
// @ts-ignore
import { CheckInItemRequestBuilderNavigationMetadata, type CheckInItemRequestBuilder } from './item/index.js';
// @ts-ignore
import { type BaseRequestBuilder, type KeysToExcludeForNavigationMetadata, type NavigationMetadata, type Parsable, type ParsableFactory, type RequestConfiguration, type RequestInformation, type RequestsMetadata } from '@microsoft/kiota-abstractions';

/**
 * Builds and executes requests for operations under /v1/check-in
 */
export interface CheckInRequestBuilder extends BaseRequestBuilder<CheckInRequestBuilder> {
    /**
     * Gets an item from the ApiSdk.v1.checkIn.item collection
     * @param id Unique identifier of the item
     * @returns {CheckInItemRequestBuilder}
     */
     byId(id: string) : CheckInItemRequestBuilder;
    /**
     * List all check ins
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {Promise<ListCheckInsResponse>}
     * @throws {Status} error when the service returns a 4XX or 5XX status code
     */
     get(requestConfiguration?: RequestConfiguration<CheckInRequestBuilderGetQueryParameters> | undefined) : Promise<ListCheckInsResponse | undefined>;
    /**
     * Create check in
     * @param body The request body
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {Promise<CheckIn>}
     * @throws {Status} error when the service returns a 4XX or 5XX status code
     */
     post(body: CreateCheckInRequest, requestConfiguration?: RequestConfiguration<object> | undefined) : Promise<CheckIn | undefined>;
    /**
     * List all check ins
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {RequestInformation}
     */
     toGetRequestInformation(requestConfiguration?: RequestConfiguration<CheckInRequestBuilderGetQueryParameters> | undefined) : RequestInformation;
    /**
     * Create check in
     * @param body The request body
     * @param requestConfiguration Configuration for the request such as headers, query parameters, and middleware options.
     * @returns {RequestInformation}
     */
     toPostRequestInformation(body: CreateCheckInRequest, requestConfiguration?: RequestConfiguration<object> | undefined) : RequestInformation;
}
/**
 * List all check ins
 */
export interface CheckInRequestBuilderGetQueryParameters {
    checkedOut?: boolean;
    checkInDate?: string;
    checkOutDate?: string;
    pageSize?: number;
    pageToken?: string;
    scope?: string;
    serviceAccount?: string;
}
/**
 * Uri template for the request builder.
 */
export const CheckInRequestBuilderUriTemplate = "{+baseurl}/v1/check-in{?checkInDate*,checkOutDate*,checkedOut*,pageSize*,pageToken*,scope*,serviceAccount*}";
/**
 * Metadata for all the navigation properties in the request builder.
 */
export const CheckInRequestBuilderNavigationMetadata: Record<Exclude<keyof CheckInRequestBuilder, KeysToExcludeForNavigationMetadata>, NavigationMetadata> = {
    byId: {
        navigationMetadata: CheckInItemRequestBuilderNavigationMetadata,
        pathParametersMappings: ["id"],
    },
};
/**
 * Metadata for all the requests in the request builder.
 */
export const CheckInRequestBuilderRequestsMetadata: RequestsMetadata = {
    get: {
        uriTemplate: CheckInRequestBuilderUriTemplate,
        responseBodyContentType: "application/json",
        errorMappings: {
            XXX: createStatusFromDiscriminatorValue as ParsableFactory<Parsable>,
        },
        adapterMethodName: "send",
        responseBodyFactory:  createListCheckInsResponseFromDiscriminatorValue,
    },
    post: {
        uriTemplate: CheckInRequestBuilderUriTemplate,
        responseBodyContentType: "application/json",
        errorMappings: {
            XXX: createStatusFromDiscriminatorValue as ParsableFactory<Parsable>,
        },
        adapterMethodName: "send",
        responseBodyFactory:  createCheckInFromDiscriminatorValue,
        requestBodyContentType: "application/json",
        requestBodySerializer: serializeCreateCheckInRequest,
        requestInformationContentSetMethod: "setContentFromParsable",
    },
};
/* tslint:enable */
/* eslint-enable */