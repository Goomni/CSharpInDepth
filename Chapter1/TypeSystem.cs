using System;
using System.Collections;
using System.Collections.Generic;

namespace Chapter1
{
    public class TypeSystem
    {
        /// <summary>
        /// 1. C#2 에서 도입된 제네릭
        ///     1.1. 제네릭 도입을 통한 세밀한 표현 가능
        ///     1.2. 불필요한 박싱 제거
        ///     3.1. 타입 안정성 강화
        /// </summary>      
        public class Book { }

        // Books 에는 어떤 타입이 저장될까? 코드만 봐서는 알 수 없다.
        public class BookShelf  
        {
            public IEnumerable Books { get; set; }

        }

        // 확실히 알 수 있으며 타입 세이프하다.
        public class BookShelfTwo
        {
            public IEnumerable<Book> Books { get; set; }

        }

        /// <summary>
        /// 2. C#2 에서 도입된 null 가능 값 타입 ( nullable value type )
        ///     2.1. 컬렉션의 인덱스나 날짜를 나타내는 DateTime 등에 특수한 의미를 가진 임의의 값을 부여할 필요가 없다.
        ///     2.2. 불필요한 정보라면 그냥 null 을 대입해주면된다.
        /// </summary>

        // 생성날짜가 정해지지 않은 경우에 표기 방법의 변화
        DateTime createdAt = DateTime.MinValue; // C#1
        DateTime? createdAtNullable = null; // C#2 

        /// <summary>
        /// 3. C#7 부터는 사용자 정의 구조체의 내용을 수정불가 - readonly struct
        ///     3.1. 컴파일러가 생성하는 코드의 효율을 개선
        ///     3.2. 개발자의 의도를 명확하게 표현 가능
        /// </summary>        
        public struct ImmutableNotReadonlyStruct
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }
        readonly public struct ImmutableReadonlyStruct
        {
            public ImmutableReadonlyStruct(double x, double y, double z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public double X { get; init; }
            public double Y { get; init; }
            public double Z { get; init; }
        }

        /// <summary>
        /// 4. C#8 부터는 null 가능 참조 타입 (nullable reference type) 추가 
        ///     4.1. 이전까지는 참조값이 null이 될 수 있음을 코드상에서 표현 불가
        ///     4.2. 세심하게 코드를 살펴보거나 기계적인 null 체크가 추가됨
        ///     4.3. 이에 C# 8 에서 참조 타입이 null이 될 수 있음을 명시적으로 선언가능
        /// </summary> 
        /// 

#nullable enable
        string GetString(string key, string? arg)
        {
            // key는 null 이 될 수없고, arg는 null이 될 수 있다.
            return key;
        }

#nullable disable

        /// <summary>
        /// 5. 단위 기능을 구현하는 데에 편의성 부여 
        ///     5.1. 익명 타입 (anonymous type)
        ///     5.2. 암시적 지역 변수 (var) 
        ///     5.3. 정적 타입 언어의 단점으로 지적되는 선언의 장황함을 해결하기 위한 장치
        ///     5.4. 특정 메서드 내에서만 사용될 뿐, 다른 곳에서는 사용되지 않는 변수와 타입을 편리하게 선언 가능 
        /// </summary> 
        /// 
        void FooVar()
        {
            // 익명 타입 
            // 이 메서드에서만 사용할 필드의 묶음            
            var book = new { Title = "소크라테스의 변명", Author = "플라톤" };

            // 컴파일러가 문법을 놓치지 않고 검사해준다.
            string title = book.Title;
            string authro = book.Author;

            // List<Book> bookList = new List<Book>() 보다 간결 
            // 암시적 지역변수 (var) 와 동적 타이핑 (dynamic typing)을 혼동해서는 안된다.
            // 아래 bookList는 변수의 타입을 명시적으로 선언하지 않았지만 여전히 정적 타입인 List<Book> 이다.
            var bookList = new List<Book>();
        }

        /// <summary>
        /// 6. 메서드의 반환 타입에서도 사용가능한 데이터의 묶음인 튜플(Tuple)
        ///     6.1. C# 7 에서 추가 
        ///     6.2. 그냥 익명 타입은 반환 값으로 사용 불가하지만 Tuple은 가능
        ///     6.3. 그럼에도 불구하고 Tuple 은 캡슐화를 지원하지 않으므로 단일 메서드 내에서만 사용하는 것을 추천        
        ///     6.4. 이런 기능들은 프로그램 전체 설계를 위한 기능이라기보다는, 부분적인 단위 기능 구현을 위한 편의기능이라고 생각하는 것을 추천
        /// </summary> 
        ///
        Tuple<string, string> FooTuple()
        {             
            // 이 메서드에서만 사용할 필드의 묶음            
            var book = new Tuple<string,string>("소크라테스의 변명", "플라톤");

            // 익명 타입과 달리 메서드 반환값으로 사용 가능
            // 그러나 단순한 데이터의 뭉치이기 때문에 캡슐화를 지원하지 않는다.
            return book;            
        }

        /// <summary>
        /// 7. Immutable 한 참조형식을 만들기 위한 record
        ///     7.1. 단순한 데이터 뭉치의 Immutable 한 참조형식을 만들기 위한 용도로 사용
        ///     7.2. Immutable 한 타입 생성시 struct 보다 record 가 효율적인 상황이 존재        
        /// </summary> 
        /// 
        public readonly struct ImmutableStruct
        {
            public ImmutableStruct(double x, double y, double z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public double X { get; init; }
            public double Y { get; init; }
            public double Z { get; init; }
        }

        // 생성구문이 간략하다.
        public record ImmutableRecord(double X, double Y, double Z);      

        public void fooRecord()
        {
            var struct1 = new ImmutableStruct(1, 1, 1); // 스택 저장
            var record1 = new ImmutableRecord(1, 1, 1); // 힙 저장 

            // 전체 필드 복사 
            var struct2 = struct1;

            // 참조만 복사 
            var record2 = record1;

            if(struct1.Equals(struct2))
            {
                // ValueType.Equals 은 리플렉션을 사용하여 모든 필드를 찾으므로 상대적으로 느리다.
            }

            if(record1.Equals(record2))
            {
                // 컴파일러가 직접 Equals 를 생성하여 값을 비교한다.
            }
        }
            
    }
}
