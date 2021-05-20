

export class User{
    id: number
    email:string
    name:string
    phone:string
    username:string
    website:string
    address: Address
    company:Company

}

export class Address{
    street:string
    suite:string
    city:string
    zipcode:string
    geo: Geo
}

export class Geo{
    lat:string
    lng:string
}
export class Company{
    bs:string
    catchPhrase:string
    name:string
}